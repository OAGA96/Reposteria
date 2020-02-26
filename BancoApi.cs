using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ApiBanco
{
    class BancoApi
    {
        Tarjeta tarjeta = new Tarjeta();
        string FechaCad = "";
        public BancoApi(Int64 noTarjeta, int verificadores, string fechacad)
        {
            tarjeta.MiTarjeta = noTarjeta;
            tarjeta.Numeros_Verificadores = verificadores;
            FechaCad = fechacad;
        }

        public async Task<Comprobante> Transferencia(Int64 tarjetaDestino, decimal monto)
        {
            DatosTransaccion datosTransaccion = new DatosTransaccion
            {
                Tarjeta_Origen = tarjeta.MiTarjeta,
                Numeros_Verificadores = tarjeta.Numeros_Verificadores,
                Fecha_Vencimiento = FechaCad,
                Tarjeta_Destino = tarjetaDestino,
                Monto = monto
            };

            var baseUrl = "https://bancossoftwarecomplejo.azurewebsites.net";
            var resource = "/api/Transaccions/Transferencia";
            var api = new RestClient(baseUrl);
            var request = new RestRequest(resource, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(datosTransaccion);

            IRestResponse response = await api.ExecuteTaskAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Comprobante comprobante = JsonConvert.DeserializeObject<Comprobante>(response.Content);
                return comprobante;
            }
            return null;
        }

        public async Task<Comprobante> Deposito(decimal monto)
        {
            DatosTransaccion datosTransaccion = new DatosTransaccion
            {
                Tarjeta_Destino = tarjeta.MiTarjeta,
                Monto = monto
            };

            var baseUrl = "https://bancossoftwarecomplejo.azurewebsites.net";
            var resource = "/api/Transaccions/Deposito";
            var api = new RestClient(baseUrl);
            var request = new RestRequest(resource, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(datosTransaccion);

            IRestResponse response = await api.ExecuteTaskAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Comprobante comprobante = JsonConvert.DeserializeObject<Comprobante>(response.Content);
                return comprobante;
            }
            return null;
        }
    }

    class DatosTransaccion
    {
        public Int64 Tarjeta_Origen { get; set; }
        public int Numeros_Verificadores { get; set; }
        public Int64 Tarjeta_Destino { get; set; }
        public string Fecha_Vencimiento { get; set; }
        public decimal Monto { get; set; }
    }

    class Comprobante
    {
        public int Id_Transaccion { get; set; }
        public DateTime Fecha { get; set; }
        public string Mensaje { get; set; }
    }

    class Tarjeta
    {
        public Int64 MiTarjeta { get; set; }
        public int Numeros_Verificadores { get; set; }
    }
}
