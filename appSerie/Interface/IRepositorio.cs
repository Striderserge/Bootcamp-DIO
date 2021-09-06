using System.Collections.Generic;

namespace appSerie.Interface
{
    public interface IRepositorio<T>
    {
         List<T> lista();
         T RetornaPorId(int id);
         void Insere(T entidade);
         void Exclui(int id);
         void Atualiza(int id, T entidade);
         int ProximoId();

    }
}