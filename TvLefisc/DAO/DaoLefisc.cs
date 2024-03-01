using Microsoft.Data.SqlClient;
using TvLefisc.Models;


namespace TvLefisc.DAO
{
    public class DaoLefisc
    {
        public DaoLefisc()
        {
            var sqlServerConnectionString = Environment.GetEnvironmentVariable("SQLServer");
        }

        public string Add1Visualizacao(int visualizacoes, int idTV)
        {
            var connString = Environment.GetEnvironmentVariable("SQLServer");
            string Retorno = "Add Visualização";

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();

                    using (SqlCommand command = new SqlCommand("UPDATE dbo.tv_Lefisc SET visualizacoes = @visualizacoes WHERE idTV = @idTV", conn))
                    {
                        command.Parameters.AddWithValue("@visualizacoes", visualizacoes + 1);
                        command.Parameters.AddWithValue("@idTV", idTV);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Retorno = "Visualização adicionada com sucesso";
                        }
                        else
                        {
                            Retorno = "Não foi possível adicionar visualização";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                Retorno = "Erro ao adicionar visualização";
            }

            return Retorno;
        }
        public List<Tv_Lefisc_categorias> GetTodasCategorias(string token)
        {
            List<Tv_Lefisc_categorias> allCategories = new List<Tv_Lefisc_categorias>();
            var connString = Environment.GetEnvironmentVariable("SQLServer");

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT idCategoria, nome FROM dbo.tv_lefisc_categorias", conn))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Tv_Lefisc_categorias categoria = new Tv_Lefisc_categorias();
                            categoria.idCategoria = (int)reader["idCategoria"];
                            categoria.nome = (string)reader["nome"];
                            allCategories.Add(categoria);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
            }
            return allCategories;
        }
        public List<Tv_Lefisc> GetPorCategoriaUltimasPorPalavra(int categoria, string palavra)
        {
            List<Tv_Lefisc> resultList = new List<Tv_Lefisc>();
            var connString = Environment.GetEnvironmentVariable("SQLServer");

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT idTV, titulo, descricao, link, visualizacoes, miniatura, data, idCategoria FROM dbo.tv_Lefisc WHERE idCategoria = @categoria AND titulo LIKE '%' + @palavra + '%' ORDER BY idTV DESC", conn))
                    {
                        cmd.Parameters.AddWithValue("@categoria", categoria);
                        cmd.Parameters.AddWithValue("@palavra", palavra);
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Tv_Lefisc record = new Tv_Lefisc();
                            record.idTv = (int)reader["idTV"];
                            record.link = reader["link"] as string;
                            record.miniatura = reader["miniatura"] as string;
                            record.titulo = reader["titulo"] as string;
                            record.descricao = reader["descricao"] as string;
                            record.visualizacoes = reader["visualizacoes"] as int? ?? 0;
                            record.data = reader["data"] as DateTime? ?? DateTime.MinValue;
                            record.idCategoria = reader["idCategoria"] as int? ?? 0;
                            resultList.Add(record);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
            }
            return resultList;
        }
        public List<Tv_Lefisc> GetPorCategoriaMaisAcessadasPorPalavra(int categoria, string palavra)
        {
            List<Tv_Lefisc> resultList = new List<Tv_Lefisc>();
            var connString = Environment.GetEnvironmentVariable("SQLServer");

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT idTV, titulo, descricao, link, visualizacoes, miniatura, data, idCategoria FROM dbo.tv_Lefisc WHERE idCategoria = @categoria AND titulo LIKE '%' + @palavra + '%' ORDER BY visualizacoes DESC", conn))
                    {
                        cmd.Parameters.AddWithValue("@categoria", categoria);
                        cmd.Parameters.AddWithValue("@palavra", palavra);
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Tv_Lefisc record = new Tv_Lefisc();
                            record.idTv = (int)reader["idTV"];
                            record.link = reader["link"] as string;
                            record.miniatura = reader["miniatura"] as string;
                            record.titulo = reader["titulo"] as string;
                            record.descricao = reader["descricao"] as string;
                            record.visualizacoes = reader["visualizacoes"] as int? ?? 0;
                            record.data = reader["data"] as DateTime? ?? DateTime.MinValue;
                            record.idCategoria = reader["idCategoria"] as int? ?? 0;
                            resultList.Add(record);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
            }
            return resultList;
        }
        public List<Tv_Lefisc> GetPorCategoriaUltimas(int categoria)
        {
            List<Tv_Lefisc> resultList = new List<Tv_Lefisc>();
            var connString = Environment.GetEnvironmentVariable("SQLServer");

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT idTV, titulo, descricao, link, visualizacoes, miniatura, data, idCategoria FROM dbo.tv_Lefisc WHERE idCategoria = @categoria ORDER BY idTV DESC", conn))
                    {
                        cmd.Parameters.AddWithValue("@categoria", categoria);
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Tv_Lefisc record = new Tv_Lefisc();
                            record.idTv = (int)reader["idTV"];
                            record.link = reader["link"] as string;
                            record.miniatura = reader["miniatura"] as string;
                            record.titulo = reader["titulo"] as string;
                            record.descricao = reader["descricao"] as string;
                            record.visualizacoes = reader["visualizacoes"] as int? ?? 0;
                            record.data = reader["data"] as DateTime? ?? DateTime.MinValue;
                            record.idCategoria = reader["idCategoria"] as int? ?? 0;
                            resultList.Add(record);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
            }
            return resultList;
        }
        public List<Tv_Lefisc> GetPorCategoriaMaisAcessadas(int categoria)
        {
            List<Tv_Lefisc> maisAcessadas = new List<Tv_Lefisc>();
            var connString = Environment.GetEnvironmentVariable("SQLServer");

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT idTV, titulo, descricao, link, visualizacoes, miniatura, data, idCategoria FROM dbo.tv_Lefisc WHERE idCategoria = @categoria ORDER BY visualizacoes DESC", conn))
                    {
                        cmd.Parameters.AddWithValue("@categoria", categoria);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Tv_Lefisc item = new Tv_Lefisc();
                                item.idTv = (int)reader["idTV"];
                                item.link = reader["link"] as string;
                                item.miniatura = reader["miniatura"] as string;
                                item.titulo = reader["titulo"] as string;
                                item.descricao = reader["descricao"] as string;
                                item.visualizacoes = reader["visualizacoes"] as int? ?? 0;
                                item.data = reader["data"] as DateTime? ?? DateTime.MinValue;
                                item.idCategoria = reader["idCategoria"] as int? ?? 0;

                                maisAcessadas.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
            }
            return maisAcessadas;
        }
        public List<Tv_Lefisc> GetPorCategoria(int categoria)
        {
            List<Tv_Lefisc> lstPorCategoria = new List<Tv_Lefisc>();
            var connString = Environment.GetEnvironmentVariable("SQLServer");

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT idTV, titulo, descricao, link, visualizacoes, miniatura, data, idCategoria FROM dbo.tv_Lefisc WHERE idCategoria = @categoria", conn))
                    {
                        cmd.Parameters.AddWithValue("@categoria", categoria);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Tv_Lefisc item = new Tv_Lefisc();
                                item.idTv = (int)reader["idTV"];
                                item.link = (string)reader["link"];
                                item.miniatura = (string)reader["miniatura"];
                                item.titulo = (string)reader["titulo"];
                                item.descricao = (string)reader["descricao"];
                                item.visualizacoes = (int)reader["visualizacoes"];
                                item.data = (DateTime)reader["data"];
                                item.idCategoria = (int)reader["idCategoria"];

                                lstPorCategoria.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string mensagemErro = ex.Message;
            }
            return lstPorCategoria;
        }
        public List<Tv_Lefisc> GetUltimasPorPalavra(string palavra)
        {
            List<Tv_Lefisc> resultList = new List<Tv_Lefisc>();
            var connString = Environment.GetEnvironmentVariable("SQLServer");

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT idTV, titulo, descricao, link, visualizacoes, miniatura, data, idCategoria FROM dbo.tv_Lefisc WHERE titulo LIKE '%' + @palavra + '%' ORDER BY idTV DESC", conn))
                    {
                        cmd.Parameters.AddWithValue("@palavra", palavra);
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Tv_Lefisc item = new Tv_Lefisc();
                            item.idTv = (int)reader["idTV"];
                            item.link = reader["link"] as string;
                            item.miniatura = reader["miniatura"] as string;
                            item.titulo = reader["titulo"] as string;
                            item.descricao = reader["descricao"] as string;
                            item.visualizacoes = reader["visualizacoes"] as int? ?? 0;
                            item.data = reader["data"] as DateTime? ?? DateTime.MinValue;
                            item.idCategoria = reader["idCategoria"] as int? ?? 0;
                            resultList.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
            }
            return resultList;
        }
        public List<Tv_Lefisc> GetTodasMaisAcessadasPorPalavra(string palavra)
        {
            var connString = Environment.GetEnvironmentVariable("SQLServer");
            List<Tv_Lefisc> resultados = new List<Tv_Lefisc>();
            
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT idTV, titulo, descricao, link, visualizacoes, miniatura, data, idCategoria FROM dbo.tv_Lefisc WHERE titulo LIKE '%' + @palavra + '%' ORDER BY visualizacoes DESC", conn))
                    {
                        cmd.Parameters.AddWithValue("@palavra", palavra);

                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Tv_Lefisc tv = new Tv_Lefisc();
                            tv.idTv = (int)reader["idTV"];
                            tv.link = (string)reader["link"];
                            tv.miniatura = (string)reader["miniatura"];
                            tv.titulo = (string)reader["titulo"];
                            tv.descricao = (string)reader["descricao"];
                            tv.visualizacoes = (int)reader["visualizacoes"];
                            tv.data = (DateTime)reader["data"];
                            tv.idCategoria = (int)reader["idCategoria"];

                            resultados.Add(tv);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string teste = ex.Message;
            }
            return resultados;
        }
        public List<Tv_Lefisc> GetUltimasCriadas()
        {
            List<Tv_Lefisc> lstTodas = new List<Tv_Lefisc>();
            var connString = Environment.GetEnvironmentVariable("SQLServer");

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT idTV, titulo, descricao, link, visualizacoes, miniatura, data, idCategoria FROM dbo.tv_Lefisc ORDER BY idTV DESC", conn))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Tv_Lefisc tv = new Tv_Lefisc();
                            tv.idTv = (int)reader["idTV"];
                            tv.link = (string)reader["link"];
                            tv.miniatura = (string)reader["miniatura"];
                            tv.titulo = (string)reader["titulo"];
                            tv.descricao = (string)reader["descricao"];
                            tv.visualizacoes = (int)reader["visualizacoes"];
                            tv.data = (DateTime)reader["data"];
                            tv.idCategoria = (int)reader["idCategoria"];

                            lstTodas.Add(tv);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string teste = ex.Message;
            }
            return lstTodas;
        }
        public List<Tv_Lefisc> GetMaisAcessada()
        {
            List<Tv_Lefisc> tvsMaisAcessadas = new List<Tv_Lefisc>();
            var connString = Environment.GetEnvironmentVariable("SQLServer");

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT idTV, titulo, descricao, link, visualizacoes, miniatura, data, idCategoria FROM dbo.tv_Lefisc ORDER BY visualizacoes DESC", conn))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Tv_Lefisc tvMaisAcessada = new Tv_Lefisc();
                            tvMaisAcessada.idTv = (int)reader["idTV"];
                            tvMaisAcessada.link = (string)reader["link"];
                            tvMaisAcessada.miniatura = (string)reader["miniatura"];
                            tvMaisAcessada.titulo = (string)reader["titulo"];
                            tvMaisAcessada.descricao = (string)reader["descricao"];
                            tvMaisAcessada.visualizacoes = (int)reader["visualizacoes"];
                            tvMaisAcessada.data = (DateTime)reader["data"];
                            tvMaisAcessada.idCategoria = (int)reader["idCategoria"];

                            tvsMaisAcessadas.Add(tvMaisAcessada);
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                string teste = ex.Message;
            }
            return tvsMaisAcessadas;
        }
        public List<Tv_Lefisc> GetTodas()
        {
            List<Tv_Lefisc> listaTodas = new List<Tv_Lefisc>();
            var connString = Environment.GetEnvironmentVariable("SQLServer");

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    using (var cmd = new SqlCommand("SELECT idTV, titulo, descricao, link, visualizacoes, miniatura, data, idCategoria FROM dbo.tv_Lefisc", conn))
                    {
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Tv_Lefisc tv = new Tv_Lefisc();
                            tv.idTv = (int)reader["idTV"];
                            tv.link = (string)reader["link"];
                            tv.miniatura = (string)reader["miniatura"];
                            tv.titulo = (string)reader["titulo"];
                            tv.descricao = (string)reader["descricao"];
                            tv.visualizacoes = (int)reader["visualizacoes"];
                            tv.data = (DateTime)reader["data"];
                            tv.idCategoria = (int)reader["idCategoria"];

                            listaTodas.Add(tv);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string teste = ex.Message;
            }
            return listaTodas;
        }
        public List<Tv_Lefisc> GetPorPalavra(string palavra)
        {
            List<Tv_Lefisc> lstPorPalavra = new List<Tv_Lefisc>();
            var connString = Environment.GetEnvironmentVariable("SQLServer");

            try
            {
                using (var conn = new SqlConnection(connString))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT idTV, titulo, descricao, link, visualizacoes, miniatura, data, idCategoria FROM dbo.tv_Lefisc WHERE titulo LIKE '%' + @palavra + '%'", conn))
                    {
                        cmd.Parameters.AddWithValue("@palavra", palavra);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Tv_Lefisc item = new Tv_Lefisc();
                                item.idTv = (int)reader["idTV"];
                                item.link = (string)reader["link"];
                                item.miniatura = (string)reader["miniatura"];
                                item.titulo = (string)reader["titulo"];
                                item.descricao = (string)reader["descricao"];
                                item.visualizacoes = (int)reader["visualizacoes"];
                                item.data = (DateTime)reader["data"];
                                item.idCategoria = (int)reader["idCategoria"];

                                lstPorPalavra.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string teste = ex.Message;
            }
            return lstPorPalavra;
        }
    }
}

