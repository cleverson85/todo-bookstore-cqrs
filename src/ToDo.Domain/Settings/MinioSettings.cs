using Microsoft.Extensions.Configuration;
using System;

namespace ToDo.Domain.Settings
{
    public class MinioSettings
    {
        public string Endpoint { get; init; }
        public bool ForceSsl { get; init; }
        public string AccessKey { get; init; }
        public string SecretKey { get; init; }
        public string Bucket { get; init; }
        public string Host { get; init; }
        public string Folder { get; init; }

        public MinioSettings(IConfiguration configuration)
        {
            Endpoint = configuration["MinioSettings:Endpoint"];
            ForceSsl = Convert.ToBoolean(configuration["MinioSettings:ForceSsl"]);
            AccessKey = configuration["MinioSettings:AccessKey"];
            SecretKey = configuration["MinioSettings:SecretKey"];
            Bucket = configuration["MinioSettings:Bucket"];
            Host = configuration["MinioSettings:Host"];
            Folder = configuration["MinioSettings:Folder"];

            //Console.WriteLine($"________{nameof(MinioSettings)}__________");
            //Console.WriteLine(MontarTextoChaveValor(nameof(Endpoint), Endpoint));
            //Console.WriteLine(MontarTextoChaveValor(nameof(ForceSsl), Convert.ToString(ForceSsl)));
            //Console.WriteLine(MontarTextoChaveValor(nameof(AccessKey), Convert.ToString(AccessKey)));
            //Console.WriteLine(MontarTextoChaveValor(nameof(SecretKey), Convert.ToString(SecretKey)));
            //Console.WriteLine(MontarTextoChaveValor(nameof(Bucket), Convert.ToString(Bucket)));
            //Console.WriteLine(MontarTextoChaveValor(nameof(Host), Convert.ToString(Host)));
            //Console.WriteLine(MontarTextoChaveValor(nameof(Folder), Convert.ToString(Folder)));
            //Console.WriteLine("___________________________________________");
        }
    }
}