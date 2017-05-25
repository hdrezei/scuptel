using System;

namespace ScupTel.Domain.Chamada
{
    public interface ISimulacaoChamadaService
    {
        ChamadaCalculada SimulacaoPlanoBasico(int dddOrigem, int dddDestino, int tempoChamada);
        ChamadaCalculada SimulacaoFaleMais(int dddOrigem, int dddDestino, int tempoChamada, Guid planoId);
    }
}
