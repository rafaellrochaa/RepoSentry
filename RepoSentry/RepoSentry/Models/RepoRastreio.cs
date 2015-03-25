using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace RepoSentry.Models
{
    public class RepoRastreio
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Sentry"].ConnectionString;

        public List<DadosRastreio> consultarDadosRastreio(string cpf, int? parCodCliente)
        {
            int replaceCodCliente = 0;
            List<DadosRastreio> dadosCliente;

            switch (parCodCliente)
            {
                //Nortecred
                case 182:
                    replaceCodCliente = 1;
                    break;

                //CETELEM
                case 175:
                    replaceCodCliente = 2;
                    break;

                //ITAU
                case 176:
                    replaceCodCliente = 3;
                    break;

                //BMG
                case 45: 
                    replaceCodCliente = 4;
                    break;
            }

            if (parCodCliente == null)
            {
                throw new Exception("Código do cliente não encontrado ou inválido. Entre em contato com suporte agilus.");
            }

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                string comando = @"select l.[originalContracts] as contratos, l.[originalOpeningDate] as dataAbertura, l.[trackCode] as codRastreio,l.[lastSituation] as ultimaSituacao
                                FROM [Sentry_v2].[dbo].[Letters] l, 
                                [Sentry_v2].[dbo].[Batches] b 
                                WHERE l.[batchesID]=b.[ID] 
                                and l.[Status]=9
                                and l.[originalCpf] = @cpf
                                and b.[companiesID] = @codCliente
                                order by [originalOpeningDate] desc";

                SqlCommand cmd = new SqlCommand(comando, conexao);

                cmd.Parameters.AddWithValue("@cpf", cpf);
                cmd.Parameters.AddWithValue("@codCliente", replaceCodCliente.ToString());
                
                conexao.Open();
                var dr = cmd.ExecuteReader();
                dadosCliente = new List<DadosRastreio>();

                while (dr.Read())
                {
                    dadosCliente.Add(new DadosRastreio() 
                    {
                        contratos = dr[0].ToString(), 
                        dataAbertura = dr[1].ToString().Substring(0,10),
                        codRastreio = dr[2].ToString(),
                        ultimaSituacao = dr[3].ToString()
                    });
                }
            }
            return dadosCliente;
        }
    }
}
