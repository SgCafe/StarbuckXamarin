using StarbuckXamarin.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StarbuckXamarin.Viewmodel
{
    public class FinishOrderViewmodel : BaseViewmodel
    {
        #region properties
        private Color _barra1Color;
        public Color Barra1Color
        {
            get => _barra1Color;
            set => SetProperty(ref _barra1Color, value);
        }

        private Color _barra2Color;
        public Color Barra2Color
        {
            get => _barra2Color;
            set => SetProperty(ref _barra2Color, value);
        }

        private Color _barra3Color;
        public Color Barra3Color
        {
            get => _barra3Color;
            set => SetProperty(ref _barra3Color, value);
        }

        private string _statusPedido;

        public string StatusPedido
        {
            get => _statusPedido;
            set => SetProperty(ref _statusPedido, value);
        }

        public string SelectedCep { get; set; }

        private TimeSpan _selectedTime;
        public TimeSpan SelectedTime
        {
            get => _selectedTime;
            set
            {
                SetProperty(ref _selectedTime, value);
                DeliveryForecast = FormatDeliveryForecast(value);
            }
        }

        private string _deliveryForecast;
        public string DeliveryForecast
        {
            get => _deliveryForecast;
            set => SetProperty(ref _deliveryForecast, value);
            
        }


        #endregion

        #region constructor
        public FinishOrderViewmodel()
        {
            Console.WriteLine($"SelectedCep in FinishOrderViewmodel: {SelectedCep}");
            Barra1Color = Color.Green;
            Barra2Color = Barra3Color = Color.Gray;
            StatusPedido = "Aguardando restaurante";
            DeliveryForecast = FormatDeliveryForecast(SelectedTime);
            Task.Run(async () => await AnimateBars());
        }

        #endregion

        #region commands

        #endregion

        #region methods

        private string FormatDeliveryForecast(TimeSpan selectedTime)
        {
            TimeSpan horarioInicial = selectedTime;

            TimeSpan horarioFinal = horarioInicial.Add(TimeSpan.FromMinutes(50));

            string horaInicialFormatada = horarioInicial.ToString("hh\\:mm");
            string horaFinalFormatada = horarioFinal.ToString("hh\\:mm");

            string previsaoEntregaFormatada = $"{horaInicialFormatada} - {horaFinalFormatada}";
            return previsaoEntregaFormatada;
        }

        private async Task AnimateBars()
        {
            while (true)
            {
                Barra1Color = Color.FromHex("#33CC33");
                StatusPedido = "Aguardando restaurante";
                await Task.Delay(5000); 

                Barra2Color = Color.FromHex("#33CC33");
                StatusPedido = "Pedido confirmado e sendo preparado";
                await Task.Delay(5000);

                Barra3Color = Color.FromHex("#33CC33");
                StatusPedido = "Pedido saiu para entrega";
                await Task.Delay(5000);

                break;
            }
        }

        #endregion
    }
}
