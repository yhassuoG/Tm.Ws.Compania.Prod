using System.Data;
using Npgsql;
using System.Collections.Generic;

using Tm.Ws.Compania.Prod.Entity;

namespace Tm.Ws.Compania.Prod.Repositories
{
    public class CompaniaRepository
    {
        private readonly string _connectionString;

        public CompaniaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<CompaniaEntity> ObtenerCompanias()
        {
            List<CompaniaEntity> companias = new List<CompaniaEntity>();

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "SELECT * FROM sisnom.tm_compania WHERE ind_compania_propia = 'S'";

                    using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;

                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CompaniaEntity compania = new CompaniaEntity
                                {
                                    CodCompania = reader.IsDBNull(reader.GetOrdinal("cod_compania")) ? "" : reader.GetString(reader.GetOrdinal("cod_compania")),
                                    DescCompania = reader.IsDBNull(reader.GetOrdinal("desc_compania")) ? "" : reader.GetString(reader.GetOrdinal("desc_compania")),
                                    CodCompaniaFact = reader.IsDBNull(reader.GetOrdinal("cod_compania_fact")) ? "" : reader.GetString(reader.GetOrdinal("cod_compania_fact")),
                                    // Agregar otros campos según sea necesario
                                };
                                companias.Add(compania);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Manejar la excepción (log, rethrow, etc.)
                    throw;
                }
            }
            return companias;
        }

        public CompaniaEntity ObtenerCompaniaPorCodigo(string codCompania)
        {
            CompaniaEntity compania = null;

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM sisnom.tm_compania WHERE cod_compania = @codCompania";

                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@codCompania", codCompania);
                    connection.Open();

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            compania = new CompaniaEntity
                            {
                                CodCompania = reader.IsDBNull(reader.GetOrdinal("cod_compania")) ? "" : reader.GetString(reader.GetOrdinal("cod_compania")),
                                DescCompania = reader.IsDBNull(reader.GetOrdinal("desc_compania")) ? "" : reader.GetString(reader.GetOrdinal("desc_compania")),
                                CodCompaniaFact = reader.IsDBNull(reader.GetOrdinal("cod_compania_fact")) ? "" : reader.GetString(reader.GetOrdinal("cod_compania_fact")),
                                // Agregar otros campos según sea necesario
                            };
                        }
                    }
                }
            }
            return compania;
        }

        public void CrearCompania(CompaniaEntity compania)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                string sql = "INSERT INTO sisnom.tm_compania (cod_compania, desc_compania, cod_compania_fact) VALUES (@codCompania, @descCompania, @codCompaniaFact)";

                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@codCompania", compania.CodCompania);
                    command.Parameters.AddWithValue("@descCompania", compania.DescCompania);
                    command.Parameters.AddWithValue("@codCompaniaFact", compania.CodCompaniaFact);
                    // Agregar otros parámetros según sea necesario

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void ActualizarCompania(CompaniaEntity compania)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                string sql = "UPDATE sisnom.tm_compania SET desc_compania = @descCompania, cod_compania_fact = @codCompaniaFact WHERE cod_compania = @codCompania";

                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@descCompania", compania.DescCompania);
                    command.Parameters.AddWithValue("@codCompaniaFact", compania.CodCompaniaFact);
                    command.Parameters.AddWithValue("@codCompania", compania.CodCompania);
                    // Agregar otros parámetros según sea necesario

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public bool EliminarCompania(string codCompania)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                string sql = "DELETE FROM sisnom.tm_compania WHERE cod_compania = @codCompania";

                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@codCompania", codCompania);
                    connection.Open();
                    return command.ExecuteNonQuery() > 0; // Retorna true si se eliminó una fila
                }
            }
        }

        public List<CompaniaEntity> ListarCompanias()
        {
            return ObtenerCompanias(); // Se puede utilizar el método existente
        }
        public List<CompaniaEntity> ObtenerDetallesCompania(string codCompania)
        {
            List<CompaniaEntity> compania = new List<CompaniaEntity>();

            // Aquí ejecutas la consulta a la base de datos para obtener los detalles de la compañía según el código proporcionado
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM sisnom.tm_compania WHERE cod_compania = @CodCompania";

                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@CodCompania", codCompania);

                    connection.Open();

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {

                        if (reader != null)
                        {
                            CompaniaEntity oCompania;
                            compania = new List<CompaniaEntity>();
                            int pDescCompania = reader.GetOrdinal("desc_compania");
                            int pRepLegal = reader.GetOrdinal("rep_legal");

                            int pRuc = reader.GetOrdinal("ruc_compania");
                            int pDireccionLegal = reader.GetOrdinal("direccion_legal");
                            int pDniRepLegal = reader.GetOrdinal("dni_rep_legal");
                            int pDireccionComercial = reader.GetOrdinal("direccion_comercial");
                            int pIndCompaniaPropia = reader.GetOrdinal("ind_compania_propia");
                            int pFechaAcogimiento = reader.GetOrdinal("fec_acogimiento");
                            int pNumAcogimiento = reader.GetOrdinal("num_acogimiento");
                            int pRegimenLaboral = reader.GetOrdinal("regimen_laboral");
                            int pIndAdmPublica = reader.GetOrdinal("ind_adm_publica");
                            int pAgenciaEmpleo = reader.GetOrdinal("ind_agencia_empleo");
                            int pIntermediacion = reader.GetOrdinal("ind_intermediacion");
                            int pAporteSenati = reader.GetOrdinal("ind_ap_senati");
                            int pCodActividad = reader.GetOrdinal("cod_actividad");
                            int pEmailCompania = reader.GetOrdinal("email_compania");
                            int pTipoCodTrab = reader.GetOrdinal("tipo_cod_trab");
                            int pCargoRepLegal = reader.GetOrdinal("cargo_rep_legal");
                            //int pLongitud = reader.GetOrdinal("longitud");
                            //int pSecuencia = reader.GetOrdinal("secuencia");
                            int pCodCompaniaFact = reader.GetOrdinal("cod_compania_fact");
                            int pCodClienteFact = reader.GetOrdinal("cod_cliente_fact");


                            while (reader.Read())
                            {
                                oCompania = new CompaniaEntity();
                                oCompania.DescCompania = reader.IsDBNull(pDescCompania) ? "" : reader.GetString(pDescCompania);
                                oCompania.RepLegal = reader.IsDBNull(pRepLegal) ? "" : reader.GetString(pRepLegal);
                                oCompania.RucCompania = reader.IsDBNull(pRuc) ? "" : reader.GetString(pRuc);
                                oCompania.DireccionLegal = reader.IsDBNull(pDireccionLegal) ? "" : reader.GetString(pDireccionLegal);
                                oCompania.DniRepLegal = reader.IsDBNull(pDniRepLegal) ? "" : reader.GetString(pDniRepLegal);
                                oCompania.DireccionComercial = reader.IsDBNull(pDireccionComercial) ? "" : reader.GetString(pDireccionComercial);
                                oCompania.IndCompaniaPropia = reader.IsDBNull(pIndCompaniaPropia) ? "" : reader.GetString(pIndCompaniaPropia);
                                oCompania.FecAcogimiento = reader.IsDBNull(pFechaAcogimiento) ? DateTime.MinValue : reader.GetDateTime(pFechaAcogimiento);
                                oCompania.NumAcogimiento = reader.IsDBNull(pNumAcogimiento) ? "" : reader.GetString(pNumAcogimiento);
                                oCompania.RegimenLaboral = reader.IsDBNull(pRegimenLaboral) ? "" : reader.GetString(pRegimenLaboral);
                                oCompania.IndAgenciaEmpleo = reader.IsDBNull(pAgenciaEmpleo) ? "" : reader.GetString(pAgenciaEmpleo);
                                oCompania.IndIntermediacion = reader.IsDBNull(pIntermediacion) ? "" : reader.GetString(pIntermediacion);
                                oCompania.IndApSenati = reader.IsDBNull(pAporteSenati) ? "" : reader.GetString(pAporteSenati);
                                oCompania.CodActividad = reader.IsDBNull(pCodActividad) ? 0 : reader.GetInt32(pCodActividad);
                                oCompania.EmailCompania = reader.IsDBNull(pEmailCompania) ? "" : reader.GetString(pEmailCompania);
                                oCompania.TipoCodTrab = reader.IsDBNull(pTipoCodTrab) ? "" : reader.GetString(pTipoCodTrab);
                                oCompania.IndAdmPublica = reader.IsDBNull(pIndAdmPublica) ? null : reader.GetString(pIndAdmPublica);
                                oCompania.CargoRepLegal = reader.IsDBNull(pCargoRepLegal) ? "" : reader.GetString(pCargoRepLegal);
                                //oCompania.Longitud = reader.IsDBNull(pLongitud) ? "" : reader.GetString(pLongitud);
                                //oCompania.Secuencia = reader.IsDBNull(pSecuencia) ? "" : reader.GetString(pSecuencia);
                                oCompania.CodCompaniaFact = reader.IsDBNull(pCodCompaniaFact) ? "" : reader.GetString(pCodCompaniaFact);
                                oCompania.CodClienteFact = reader.IsDBNull(pCodClienteFact) ? "" : reader.GetString(pCodClienteFact);



                                compania.Add(oCompania);
                            }
                        }
                    }
                }
            }

            return compania;

        }

    }
}
