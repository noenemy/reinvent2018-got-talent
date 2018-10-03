using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;

namespace GotTalent_API.Utils
{
    public class RekognitionUtil
    {
        public async static void EvaluateEmotionScore(IAmazonRekognition rekognitionClient, string bucketName, string keyName)
        {
            FaceDetail result = null;
            DetectFacesRequest detectFacesRequest = new DetectFacesRequest()
            {
                Image = new Image {
                    S3Object = new S3Object {
                        Bucket = bucketName,
                        Name = keyName
                    }
                },
                Attributes = new List<String>() { "ALL" }
            };

            try
            {
                Task<DetectFacesResponse> detectTask = rekognitionClient.DetectFacesAsync(detectFacesRequest);
                DetectFacesResponse detectFacesResponse = await detectTask;

                PrintFaceDetails(detectFacesResponse.FaceDetails);

                if (detectFacesResponse.FaceDetails.Count > 0)
                    result = detectFacesResponse.FaceDetails[0]; // take the 1st face only
            }
            catch (AmazonRekognitionException rekognitionException)
            {
                Console.WriteLine(rekognitionException.Message, rekognitionException.InnerException);
            }
        }

        public async static void EvaluateEmotionScore(IAmazonRekognition rekognitionClient, MemoryStream stream)
        {
            FaceDetail result = null;
            DetectFacesRequest detectFacesRequest = new DetectFacesRequest()
            {
                Image = new Image {
                    Bytes = stream
                },
                Attributes = new List<String>() { "ALL" }
            };

            try
            {
                Task<DetectFacesResponse> detectTask = rekognitionClient.DetectFacesAsync(detectFacesRequest);
                DetectFacesResponse detectFacesResponse = await detectTask;

                PrintFaceDetails(detectFacesResponse.FaceDetails);

                if (detectFacesResponse.FaceDetails.Count > 0)
                    result = detectFacesResponse.FaceDetails[0]; // take the 1st face only
            }
            catch (AmazonRekognitionException rekognitionException)
            {
                Console.WriteLine(rekognitionException.Message, rekognitionException.InnerException);
            }
        }

        private static void PrintFaceDetails(List<FaceDetail> faceDetails)
        {
            foreach(FaceDetail face in faceDetails)
            {
                Console.WriteLine("BoundingBox: top={0} left={1} width={2} height={3}", face.BoundingBox.Left,
                    face.BoundingBox.Top, face.BoundingBox.Width, face.BoundingBox.Height);
                Console.WriteLine("Confidence: {0}\nLandmarks: {1}\nPose: pitch={2} roll={3} yaw={4}\nQuality: {5}",
                    face.Confidence, face.Landmarks.Count, face.Pose.Pitch,
                    face.Pose.Roll, face.Pose.Yaw, face.Quality);
                Console.WriteLine("The detected face is estimated to be between " +
                    face.AgeRange.Low + " and " + face.AgeRange.High + " years old.");
            }
        }
    }
}