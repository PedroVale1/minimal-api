using Microsoft.VisualStudio.TestTools.UnitTesting;
using minimal_api.Dominio.Entidades;
using Test.Domain.Entidades;

namespace Test.Domain.Entidades
{
    [TestClass]  // Certifique-se de que a classe de teste está anotada com [TestClass]
    public class AdministradorTest
    {
        [TestMethod]
        public void TestarGetSetPropriedades()
        {
            // Arrange
            var adm = new Administrador();

            // Act
            adm.Id = 1;
            adm.Email = "teste@teste.com";
            adm.Senha = "teste";
            adm.Perfil = "Adm";

            // Assert
            Assert.AreEqual(1, adm.Id);
            Assert.AreEqual("teste@teste.com", adm.Email);
            Assert.AreEqual("teste", adm.Senha); // Corrigido para verificar o valor correto
            Assert.AreEqual("Adm", adm.Perfil);
        }
    }
}
