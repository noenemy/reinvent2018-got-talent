namespace GotTalent_API.DTOs
{
    public class GameStagePostImageDTO
    {
        public int gameId { get; set; }
        public string actionType { get; set; }
        public string base64Image { get; set; }
    }
}