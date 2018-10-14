using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using Amazon.S3;
using GotTalent_API.Data;
using GotTalent_API.DTOs;
using GotTalent_API.Models;
using GotTalent_API.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GotTalent_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StageLogsController : ControllerBase
    {
        private DataContext _context;
        IAmazonS3 S3Client { get; set; }
        IAmazonRekognition RekognitionClient { get; set; }        

        public StageLogsController(DataContext context, IAmazonS3 s3Client, IAmazonRekognition rekognitionClient)
        {
            _context = context;
            this.S3Client = s3Client;
            this.RekognitionClient = rekognitionClient;            
        }

        // GET api/stagelogs
        [HttpGet]
        public async Task<IActionResult> GetStageLogs()
        {
            var values = await _context.StageLog.ToListAsync();
            return Ok(values);
        }

        // GET api/stagelogs/5
        [HttpGet("{game_id}")]
        public async Task<IActionResult> GetStageLog(int game_id)
        {
            var values = await _context.StageLog.Where(x => x.game_id == game_id).ToListAsync();
            return Ok(values);
        }

        // POST api/stagelogs
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GameStagePostImageDTO dto)
        {
            Console.WriteLine("PostImage entered.");

            string bucketName = "reinvent-gottalent";

            // Retrieving image data
            // ex: game/10/Happiness.jpg
            string keyName = string.Format("game/{0}/{1}.jpg", dto.gameId, dto.actionType);
            byte[] imageByteArray = Convert.FromBase64String(dto.base64Image);
            if (imageByteArray.Length == 0)
                return BadRequest("Image length is 0.");

            // Image manipulation (TODO: resize)

            StageLog newStageLog = null;
            // Evaluate rekognition metrics
            using (MemoryStream ms = new MemoryStream(imageByteArray))
            {
                // call Rekonition API
                FaceDetail faceDetail = await RekognitionUtil.GetFaceDetailFromStream(this.RekognitionClient, ms);   

                // Upload image to S3 bucket
                await Task.Run(() => S3Util.UploadToS3(this.S3Client, bucketName, keyName, ms));

                // Get a specific emotion score
                double emotionScore = 0.0f;
                if (dto.actionType != "Profile")
                {
                    emotionScore = RekognitionUtil.GetEmotionScore(faceDetail.Emotions, dto.actionType);
                }

                int evaluatedAge = (faceDetail.AgeRange.High + faceDetail.AgeRange.Low) / 2;
                string evaluatedGender = faceDetail.Gender.Value;

                // Database update
                newStageLog = new StageLog{
                    game_id = dto.gameId,
                    action_type = dto.actionType,
                    score = emotionScore,
                    file_loc = keyName,
                    age = evaluatedAge,
                    gender = evaluatedGender,
                    log_date = DateTime.Now 
                };

                Console.WriteLine("==== new stage log ====");
                Console.WriteLine(newStageLog);

                var value = _context.StageLog.Add(newStageLog);
                await _context.SaveChangesAsync();  
            }

            // Send response

            return Ok(newStageLog);            
        }

        // PUT api/stagelogs/5
        [HttpPut("{game_id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/stagelogs/5
        [HttpDelete("{game_id}")]
        public void Delete(int id)
        {
        }        
    }
}