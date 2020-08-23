using appBuscaCep.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace appBuscaCep.Services
{
    public class ViaCepService
    {
        private static readonly string apiEndereco = "https://viacep.com.br/ws/{0}/json/";

        public static Endereco BuscarEnderecoViaCep(string cep)
        {
            Endereco endereco;
            string novoEnderecoUrl = string.Format(apiEndereco, cep);
            try
            {
                using (var requisicao = new HttpClient())
                {
                    var resposta = requisicao.GetStringAsync(novoEnderecoUrl).Result;

                    return endereco = JsonConvert.DeserializeObject<Endereco>(resposta);
                }
            }
            catch (Exception erro)
            {
                return null;
            }
        }
    }
}
