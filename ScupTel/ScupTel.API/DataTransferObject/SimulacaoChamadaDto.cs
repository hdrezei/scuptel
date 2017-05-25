using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScupTel.API.DataTransferObject
{
    public class SimulacaoChamadaDto
    {
        public SimulacaoChamadaDto(int origem, int destino, int tempo, string planoFaleMais, decimal? comFaleMais, decimal? semFaleMais)
        {
            Origem = origem;
            Destino = destino;
            Tempo = tempo;
            PlanoFaleMais = planoFaleMais;
            ComFaleMais = comFaleMais;
            SemFaleMais = semFaleMais;
        }

        public int Origem { get; set; }
        public int Destino { get; set; }
        public int Tempo { get; set; }
        public string PlanoFaleMais { get; set; }
        public decimal? ComFaleMais { get; set; }
        public decimal? SemFaleMais { get; set; }
    }
}
