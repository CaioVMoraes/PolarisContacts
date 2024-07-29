using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.Domain.Settings;
using SQLitePCL;
using System.Data;
using System.Data.SqlClient;

namespace PolarisContacts.Infrastructure.Repositories
{
    public class DatabaseConnection(IOptions<DbSettings> dbSettings) : IDatabaseConnection
    {
        private readonly DbSettings _dbSettings = dbSettings.Value;

        public IDbConnection AbrirConexao()
        {
            if (_dbSettings.ConnectionString.Contains("Data Source=:memory:"))
            {
                // Inicializa o SQLite
                Batteries.Init();

                // Usando SQLite para testes
                var connection = new SqliteConnection(_dbSettings.ConnectionString);
                connection.Open();

                // Inicializa o banco de dados
                InitializeDatabase(connection);

                return connection;
            }
            else
            {
                // Usando SQL Server para ambiente de produção
                var connection = new SqlConnection(_dbSettings.ConnectionString);
                connection.Open();
                return connection;
            }
        }

        private void InitializeDatabase(IDbConnection connection)
        {
            // Cria as tabelas de acordo com a estrutura fornecida
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                CREATE TABLE Usuarios (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    [Login] TEXT NOT NULL,
                    Senha TEXT NOT NULL,
                    Ativo BOOLEAN
                );

                INSERT INTO Usuarios (Login, Senha, Ativo) VALUES ('Login Teste', 1, 1);
                INSERT INTO Usuarios (Login, Senha, Ativo) VALUES ('Login Teste2', 2, 1);

                CREATE TABLE Contatos (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    IdUsuario INTEGER NOT NULL,
                    Nome TEXT NOT NULL,
                    Ativo BOOLEAN,
                    FOREIGN KEY (IdUsuario) REFERENCES Usuarios(Id)
                );

                -- Inserção de Dados na Tabela Contatos
                INSERT INTO Contatos (IdUsuario, Nome, Ativo) VALUES (1, 'Contato Teste', 1);
                INSERT INTO Contatos (IdUsuario, Nome, Ativo) VALUES (2, 'Contato Teste', 1);

                CREATE TABLE Regioes (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    DDD TEXT NOT NULL,
                    NomeRegiao TEXT NOT NULL,
                    Ativo BOOLEAN
                );

                -- Inserção de Dados na Tabela Regioes
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('011', 'São Paulo', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('012', 'São José dos Campos', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('013', 'Santos', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('014', 'Bauru', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('015', 'Sorocaba', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('016', 'Ribeirão Preto', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('017', 'São José do Rio Preto', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('018', 'Presidente Prudente', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('021', 'Rio de Janeiro', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('022', 'Campos dos Goytacazes', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('024', 'Volta Redonda', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('027', 'Vitória', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('028', 'Cachoeiro de Itapemirim', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('031', 'Belo Horizonte', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('032', 'Juiz de Fora', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('033', 'Governador Valadares', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('034', 'Uberlândia', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('035', 'Poços de Caldas', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('037', 'Divinópolis', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('038', 'Montes Claros', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('041', 'Curitiba', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('042', 'Ponta Grossa', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('043', 'Londrina', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('044', 'Maringá', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('045', 'Foz do Iguaçu', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('046', 'Francisco Beltrão', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('047', 'Joinville', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('048', 'Florianópolis', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('049', 'Chapecó', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('051', 'Porto Alegre', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('053', 'Pelotas', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('054', 'Caxias do Sul', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('055', 'Santa Maria', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('061', 'Brasília', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('062', 'Goiânia', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('064', 'Rio Verde', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('065', 'Cuiabá', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('066', 'Rondonópolis', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('067', 'Campo Grande', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('068', 'Rio Branco', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('069', 'Porto Velho', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('071', 'Salvador', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('073', 'Ilhéus', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('074', 'Juazeiro', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('075', 'Feira de Santana', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('077', 'Barreiras', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('079', 'Aracaju', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('081', 'Recife', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('082', 'Maceió', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('083', 'João Pessoa', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('084', 'Natal', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('085', 'Fortaleza', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('086', 'Teresina', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('087', 'Petrolina', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('088', 'Juazeiro do Norte', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('089', 'Picos', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('091', 'Belém', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('092', 'Manaus', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('093', 'Santarém', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('094', 'Marabá', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('095', 'Boa Vista', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('096', 'Macapá', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('097', 'Coari', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('098', 'São Luís', 1);
                INSERT INTO Regioes (DDD, NomeRegiao, Ativo) VALUES ('099', 'Imperatriz', 1);

                CREATE TABLE Telefones (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    IdRegiao INTEGER NOT NULL,
                    IdContato INTEGER NOT NULL,
                    NumeroTelefone TEXT NOT NULL,
                    Ativo BOOLEAN,
                    FOREIGN KEY (IdContato) REFERENCES Contatos(Id)
                );

                CREATE TABLE Celulares (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    IdRegiao INTEGER NOT NULL,
                    IdContato INTEGER NOT NULL,
                    NumeroCelular TEXT NOT NULL,
                    Ativo BOOLEAN,
                    FOREIGN KEY (IdContato) REFERENCES Contatos(Id)
                );

                CREATE TABLE Emails (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    IdContato INTEGER NOT NULL,
                    EnderecoEmail TEXT NOT NULL,
                    Ativo BOOLEAN,
                    FOREIGN KEY (IdContato) REFERENCES Contatos(Id)
                );

                CREATE TABLE Enderecos (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    IdContato INTEGER NOT NULL,
                    Logradouro TEXT,
                    Numero TEXT,
                    Cidade TEXT,
                    Estado TEXT,
                    Bairro TEXT,
                    Complemento TEXT,
                    CEP TEXT,
                    Ativo BOOLEAN,
                    FOREIGN KEY (IdContato) REFERENCES Contatos(Id)
                );
            ";
                command.ExecuteNonQuery();
            }
        }
    }
}

