using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using minimal_api.Dominio.DTOs;
using minimal_api.Dominio.Entidades;
using minimal_api.Dominio.Servicos;
using minimal_api.Infraestrutura.DB;


namespace Test.Domain.Servicos
{
    [TestClass]
    public class AdministradorServicoTest
    {
        private DbContexto CriarContextoDeTeste()
        {
            var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var path = Path.GetFullPath(Path.Combine(assemblyPath ?? "", "..", "..", ".."));


            var builder = new ConfigurationBuilder()
            .SetBasePath(path ?? Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

            var configuration = builder.Build();

            return new DbContexto(configuration);
        }

        [TestMethod]
        public void TestandoSalvarAdministrador()
        {
            // Arrange
            var context = CriarContextoDeTeste();
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE Administradores");


            var adm = new Administrador();
            adm.Id = 1;
            adm.Email = "teste@teste.com";
            adm.Senha = "teste";
            adm.Perfil = "Adm";

            var administradorServico = new AdministradorServico(context);

            // Act
            administradorServico.Incluir(adm);

            // Assert
            Assert.AreEqual(1, administradorServico.Todos(1).Count());
        }

        public void TestandoBuscaPorIdAdministrador()
        {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Administradores");

        var adm = new Administrador();
        adm.Email = "teste@teste.com";
        adm.Senha = "teste";
        adm.Perfil = "Adm";

        var administradorServico = new AdministradorServico(context);

        // Act
        administradorServico.Incluir(adm);
        var admDoBanco = administradorServico.BuscaPorId(adm.Id);

        // Assert
        Assert.AreEqual(1, admDoBanco?.Id);
    }

     [TestMethod]
        public void TestandoLoginAdministrador()
        {
            // Arrange
            var context = CriarContextoDeTeste();
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE Administradores");

            var adm = new Administrador
            {
                Id = 1,
                Email = "teste@teste.com",
                Senha = "teste",
                Perfil = "Adm"
            };

            var administradorServico = new AdministradorServico(context);
            administradorServico.Incluir(adm);

            var loginDTO = new LoginDTO
            {
                Email = "teste@teste.com",
                Senha = "teste"
            };

            // Act
            var resultado = administradorServico.Login(loginDTO);

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual(adm.Email, resultado?.Email);
            Assert.AreEqual(adm.Senha, resultado?.Senha);
        }

        [TestMethod]
        public void TestandoListagemAdministradores()
        {
            // Arrange
            var context = CriarContextoDeTeste();
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE Administradores");

            var administradorServico = new AdministradorServico(context);

            for (int i = 1; i <= 15; i++)
            {
                var adm = new Administrador
                {
                    Id = i,
                    Email = $"teste{i}@teste.com",
                    Senha = "teste",
                    Perfil = "Adm"
                };

                administradorServico.Incluir(adm);
            }

            // Act
            var resultado = administradorServico.Todos(1); // Página 1

            // Assert
            Assert.AreEqual(10, resultado.Count); // Deve retornar 10 itens por padrão
        }
    }
}
