namespace Helika
{
    public class HelikaBaseURL
    {
        public const string Production = "https://api.helika.io/v1";
        public const string Develop = "https://api-stage.helika.io/v1";
        public const string Localhost = "http://localhost:8181/v1";

        public static bool validate(string baseUrl)
        {
            return baseUrl == Production || baseUrl == Develop || baseUrl == Localhost;
        }
    }
}