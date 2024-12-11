namespace Tm.Ws.Compania.Prod.Entity
{
    public class CompaniaEntity
    {
        public string CodCompania { get; set; }
        public string DescCompania { get; set; }
        public string RucCompania { get; set; }
        public string RepLegal { get; set; }
        public string DireccionLegal { get; set; }
        public string DireccionComercial { get; set; }
        public string TipoCodTrab { get; set; }
        public string IndCompaniaPropia { get; set; }
        public string CodCompaniaFact { get; set; }
        public string CodClienteFact { get; set; }
        public string DniRepLegal { get; set; }
        public DateTime? FecAcogimiento { get; set; }
        public string NumAcogimiento { get; set; }
        public string RegimenLaboral { get; set; }
        public string IndAdmPublica { get; set; }
        public string IndAgenciaEmpleo { get; set; }
        public string IndIntermediacion { get; set; }
        public string IndApSenati { get; set; }
        public string EmailCompania { get; set; }
        public string CargoRepLegal { get; set; }
        public string ImgLogo { get; set; }
        public string MensajeDetalle { get; set; }
        public int Rpta { get; set; }
        // Actividades Compañía
        public int CodActividad { get; set; }
        public string DescActividad { get; set; }
        public int Estado { get; set; }
    
    }
}
