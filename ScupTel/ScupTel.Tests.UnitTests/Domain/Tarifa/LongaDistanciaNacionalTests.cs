using ScupTel.Domain.Core.Helpers;
using ScupTel.Domain.Tarifa;
using ScupTel.Domain.Localidade;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssertions;
using ScupTel.Domain.Chamada;

namespace ScupTel.Tests.UnitTests.Domain.Tarifa
{
    public class LongaDistanciaNacionalTests
    {
        public LongaDistanciaNacionalTests()
        {
            TestConsoleWriterHelper.PrintTestClass("LongaDistanciaNacionalTests");
        }

        [Fact]
        public void TestaConstrutorDoObjeto()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo construtor do objeto LongaDistanciaNacional");

            var cobertura = new Cobertura(11, 16);
            var dataInicioVigencia = DateTime.Now.AddMonths(-1);
            var dataFinalVigencia = DateTime.Now.AddMonths(1);

            var longaDistanciaNacional = new LongaDistanciaNacional("Tarifa de Teste LongaDistanciaNacional", cobertura, dataInicioVigencia, dataFinalVigencia, 1.25m);

            TestConsoleWriterHelper.PrintAssertResult("O nome da tarifa deve ser 'Tarifa de Teste LongaDistanciaNacional'");
            longaDistanciaNacional.Nome.Should().Be("Tarifa de Teste LongaDistanciaNacional");
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("O ddd de origem da cobertura deve ser '11'");
            longaDistanciaNacional.Cobertura.DddOrigem.Should().Be(11);
            TestConsoleWriterHelper.TestPassed();
            
            TestConsoleWriterHelper.PrintAssertResult("O ddd de destino da cobertura deve ser '16'");
            longaDistanciaNacional.Cobertura.DddDestino.Should().Be(16);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("O inicio da vigencia deve ser igual " + dataInicioVigencia.ToString("yyy-MM-dd hh:mm:ss"));
            longaDistanciaNacional.InicioVigencia.Should().Be(dataInicioVigencia);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("O fim da vigencia deve ser igual " + dataFinalVigencia.ToString("yyy-MM-dd hh:mm:ss"));
            longaDistanciaNacional.FimVigencia.Should().Be(dataFinalVigencia);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("O valor base de calculo deve ser igual '1.25'");
            longaDistanciaNacional.ValorBaseCalculo.Should().Be(1.25m);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("O resumo de tarifas aplicadas nao pode ser nulo");
            longaDistanciaNacional.ResumoTarifasAplicadas.Should().NotBeNull();
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public void TestaOsMetodosInternos()
        {
            var cobertura = new Cobertura(11, 16);
            var dataInicioVigencia = DateTime.Now.AddMonths(-1);
            var dataFinalVigencia = DateTime.Now.AddMonths(1);

            var longaDistanciaNacional = new LongaDistanciaNacional("Tarifa de Teste LongaDistanciaNacional", cobertura, dataInicioVigencia, dataFinalVigencia, 1.25m);
            var chamadaRealizada = new ChamadaRealizada(cobertura, DateTime.Now, new TimeSpan(0, 20, 0));
            var chamadaTarifada = new ChamadaTarifada(chamadaRealizada);

            TestConsoleWriterHelper.PrintAssertResult("O metodo ValidarTipoDaTarifa deve retornar vardadeiro");
            longaDistanciaNacional.ValidarTipoDaTarifa(longaDistanciaNacional).Should().Be(true);
            TestConsoleWriterHelper.TestPassed();
            
            TestConsoleWriterHelper.PrintAssertResult("O metodo CalcularTarifaChamada deve retornar o valor base de calculo");
            longaDistanciaNacional.CalcularTarifaChamada(longaDistanciaNacional).Should().Be(1.25m);
            TestConsoleWriterHelper.TestPassed();
            
            TestConsoleWriterHelper.PrintAssertResult("O metodo TarifaVigente deve retornar verdadeiro");
            longaDistanciaNacional.TarifaVigente(chamadaTarifada).Should().Be(true);
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public void TestaOsMetodoTarifaCalculadaQuandoAplicada()
        {
            var cobertura = new Cobertura(11, 16);
            var dataInicioVigencia = DateTime.Now.AddMonths(-1);
            var dataFinalVigencia = DateTime.Now.AddMonths(1);

            var longaDistanciaNacional = new LongaDistanciaNacional("Tarifa de Teste LongaDistanciaNacional", cobertura, dataInicioVigencia, dataFinalVigencia, 1.25m);
            var chamadaRealizada = new ChamadaRealizada(cobertura, DateTime.Now, new TimeSpan(0, 20, 0));
            var chamadaTarifada = new ChamadaTarifada(chamadaRealizada);

            var resumoCalculoTarifaChamada = longaDistanciaNacional.TarifaCalculada(chamadaTarifada);

            TestConsoleWriterHelper.PrintAssertResult("A ChamadaTarifada nao deve ser nula");
            resumoCalculoTarifaChamada.ChamadaTarifada.Should().NotBeNull();
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("A TarifaChamada nao deve ser nula");
            resumoCalculoTarifaChamada.TarifaChamada.Should().NotBeNull();
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("O valor dever ser '1.25'");
            resumoCalculoTarifaChamada.Valor.Should().Be(1.25m);
            TestConsoleWriterHelper.TestPassed();
            
            TestConsoleWriterHelper.PrintAssertResult("A propriedade 'aplicado' dever ser verdadeiro");
            resumoCalculoTarifaChamada.Aplicado.Should().Be(true);
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public void TestaOsMetodoTarifaCalculadaQuandoNaoAplicada()
        {
            var cobertura = new Cobertura(11, 16);
            var dataInicioVigencia = DateTime.Now.AddMonths(-2);
            var dataFinalVigencia = DateTime.Now.AddMonths(-1);

            var longaDistanciaNacional = new LongaDistanciaNacional("Tarifa de Teste LongaDistanciaNacional", cobertura, dataInicioVigencia, dataFinalVigencia, 1.25m);
            var chamadaRealizada = new ChamadaRealizada(cobertura, DateTime.Now, new TimeSpan(0, 20, 0));
            var chamadaTarifada = new ChamadaTarifada(chamadaRealizada);

            var resumoCalculoTarifaChamada = longaDistanciaNacional.TarifaCalculada(chamadaTarifada);
            
            TestConsoleWriterHelper.PrintAssertResult("A propriedade 'aplicado' dever ser falso por nao estar mais vigente");
            resumoCalculoTarifaChamada.Aplicado.Should().Be(false);
            TestConsoleWriterHelper.TestPassed();
        }
    }
}
