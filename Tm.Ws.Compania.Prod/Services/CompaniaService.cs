using System.Collections.Generic;
using System.Linq;
using Tm.Ws.Compania.Prod.Entity;
using Tm.Ws.Compania.Prod.Repositories;

namespace Tm.Ws.Compania.Prod.Services
{
    public class CompaniaService
    {
        private readonly CompaniaRepository _companiaRepository;

        public CompaniaService(CompaniaRepository companiaRepository)
        {
            _companiaRepository = companiaRepository ?? throw new ArgumentNullException(nameof(companiaRepository));
        }


        public List<CompaniaEntity> ObtenerCompanias()
        {
            return _companiaRepository.ObtenerCompanias();
        }

        public CompaniaEntity ObtenerCompaniaPorCodigo(string codCompania)
        {
            return _companiaRepository.ObtenerCompaniaPorCodigo(codCompania);
        }

        public void CrearCompania(CompaniaEntity compania)
        {
            _companiaRepository.CrearCompania(compania);
        }

        public void ActualizarCompania(CompaniaEntity compania)
        {
            _companiaRepository.ActualizarCompania(compania);
        }

        public bool EliminarCompania(string codCompania)
        {
            return _companiaRepository.EliminarCompania(codCompania);
        }

        public List<CompaniaEntity> ListarCompanias()
        {
            return _companiaRepository.ListarCompanias();
        }

        public List<CompaniaEntity> ObtenerDetalleCompanias(string codCompania)
        {
            //
            return _companiaRepository.ObtenerDetallesCompania(codCompania);
        }
    }
}
