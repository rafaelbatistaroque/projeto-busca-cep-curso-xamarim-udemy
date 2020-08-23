using appBuscaCep.Models;
using appBuscaCep.Services;
using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace appBuscaCep
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            btnBuscar.Clicked += BuscarCep;
        }

        private void BuscarCep(object sender, EventArgs e)
        {
            var cep = txtCep.Text.Trim();
            if (EhCepValido(cep))
            {
                Endereco endereco = ViaCepService.BuscarEnderecoViaCep(cep);

                if (EhEnderecoValido(endereco))
                {
                    lblResultado.Text = $"Endereço: {endereco.Localidade} \n" +
                                        $"UF: {endereco.Uf} \n" +
                                        $"Bairro: {endereco.Bairro} ";
                };

                txtCep.Text = string.Empty;
            }
        }

        private bool EhCepValido(string cep)
        {
            var padraoCep = new Regex(@"\d{5}[\s-.]?\d{3}");
            var ehCepValido = padraoCep.Match(cep).Success;

            return ehCepValido || AlertaErro("Erro", "Cep digitado incorretamente.");
        }

        private bool AlertaErro(string titulo, string mensagem)
        {
            DisplayAlert(titulo, mensagem, "OK");
            return false;
        }

        private bool EhEnderecoValido(Endereco endereco)
        {
            if (string.IsNullOrEmpty(endereco.Cep))
            {
                DisplayAlert("Erro", $"Endereço não encontrado para o CEP {txtCep.Text}.", "Ok");
                return false;
            }
            return true;
        }
    }
}
