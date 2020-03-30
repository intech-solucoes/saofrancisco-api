using Intech.Lib.Util.Date;
using Intech.PrevSystem.Negocio.Proxy;
using System;
using System.Linq;

namespace Intech.PrevSystem.Negocio.Saofrancisco.Simuladores
{
    public class BaseSimulador
    {
        public List<KeyValuePair<string, string>> MemoriaCalculo { get; set; }

        public void Add(string chave, string valor) =>
            MemoriaCalculo.Add(new KeyValuePair<string, string>(chave, valor));
    }
}