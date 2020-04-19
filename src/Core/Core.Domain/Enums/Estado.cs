using System.ComponentModel;

namespace Core.Domain.Enums
{
    public enum EstadoEnum
    {
        // Região Norte
        [Description("Rondônia")]
        Rondonia = 11,
        [Description("Acre")]
        Acre = 12,
        [Description("Amazonas")]
        Amazonas = 13,
        [Description("Roraima")]
        Roraima = 14,
        [Description("Para")]
        Para = 15,
        [Description("Amapa")]
        Amapa = 16,
        [Description("Tocantins")]
        Tocantins = 17,

        // Região Nordeste
        [Description("Maranhão")]
        Maranhao = 21,
        [Description("Piauí")]
        Piaui = 22,
        [Description("Ceará")]
        Ceara = 23,
        [Description("Rio Grande do Norte")]
        RioGrandeDoNorte = 24,
        [Description("Paraíba")]
        Paraiba = 25,
        [Description("Pernambuco")]
        Pernambuco = 26,
        [Description("Alagoas")]
        Alagoas = 27,
        [Description("Sergipe")]
        Sergipe = 28,
        [Description("Bahia")]
        Bahia = 29,

        // Região Sudeste
        [Description("Minas Gerais")]
        MinasGerais = 31,
        [Description("Espírito Santo")]
        EspiritoSanto = 32,
        [Description("Rio de Janeiro")]
        RioDeJaneiro = 33,
        [Description("São Paulo")]
        SaoPaulo = 35,

        // Região Sul
        [Description("Paraná")]
        Parana = 41,
        [Description("Santa Catarina")]
        SantaCatarina = 42,
        [Description("Rio Grande do Sul")]
        RioGrandeDoSul = 43,

        // Região Centro-Oeste
        [Description("Mato Grosso do Sul")]
        MatoGrossoDoSul = 50,
        [Description("Mato Grosso")]
        MatoGrosso = 51,
        [Description("Goias")]
        Goias = 52,
        [Description("Distrito Federal")]
        DistritoFederal = 53
    }

    public static class EstadoExtensions
    {
        public static string ToSigla(this EstadoEnum estadoEnum)
        {
            switch (estadoEnum)
            {
                case EstadoEnum.Rondonia: return "RO";
                case EstadoEnum.Acre: return "AC";
                case EstadoEnum.Amazonas: return "AM";
                case EstadoEnum.Roraima: return "RR";
                case EstadoEnum.Para: return "PA";
                case EstadoEnum.Amapa: return "AP";
                case EstadoEnum.Tocantins: return "TO";

                case EstadoEnum.Maranhao: return "MA";
                case EstadoEnum.Piaui: return "PI";
                case EstadoEnum.Ceara: return "CE";
                case EstadoEnum.RioGrandeDoNorte: return "RN";
                case EstadoEnum.Paraiba: return "PB";
                case EstadoEnum.Pernambuco: return "PE";
                case EstadoEnum.Alagoas: return "AL";
                case EstadoEnum.Sergipe: return "SE";
                case EstadoEnum.Bahia: return "BA";

                case EstadoEnum.MinasGerais: return "MG";
                case EstadoEnum.EspiritoSanto: return "ES";
                case EstadoEnum.RioDeJaneiro: return "RJ";
                case EstadoEnum.SaoPaulo: return "SP";

                case EstadoEnum.Parana: return "PR";
                case EstadoEnum.SantaCatarina: return "SC";
                case EstadoEnum.RioGrandeDoSul: return "RS";

                case EstadoEnum.MatoGrossoDoSul: return "MS";
                case EstadoEnum.MatoGrosso: return "MT";
                case EstadoEnum.Goias: return "GO";

                default: return "DF";
            }
        }

        public static EstadoEnum ToEstadoEnum(string sigla)
        {
            switch (sigla)
            {
                case "RO": return EstadoEnum.Rondonia;
                case "AC": return EstadoEnum.Acre;
                case "AM": return EstadoEnum.Amazonas;
                case "RR": return EstadoEnum.Roraima;
                case "PA": return EstadoEnum.Para;
                case "AP": return EstadoEnum.Amapa;
                case "TO": return EstadoEnum.Tocantins;

                case "MA": return EstadoEnum.Maranhao;
                case "PI": return EstadoEnum.Piaui;
                case "CE": return EstadoEnum.Ceara;
                case "RN": return EstadoEnum.RioGrandeDoNorte;
                case "PB": return EstadoEnum.Paraiba;
                case "PE": return EstadoEnum.Pernambuco;
                case "AL": return EstadoEnum.Alagoas;
                case "SE": return EstadoEnum.Sergipe;
                case "BA": return EstadoEnum.Bahia;

                case "MG": return EstadoEnum.MinasGerais;
                case "ES": return EstadoEnum.EspiritoSanto;
                case "RJ": return EstadoEnum.RioDeJaneiro;
                case "SP": return EstadoEnum.SaoPaulo;

                case "PR": return EstadoEnum.Parana;
                case "SC": return EstadoEnum.SantaCatarina;
                case "RS": return EstadoEnum.RioGrandeDoSul;

                case "MS": return EstadoEnum.MatoGrossoDoSul;
                case "MT": return EstadoEnum.MatoGrosso;
                case "GO": return EstadoEnum.Goias;

                default: return EstadoEnum.DistritoFederal;
            }
        }
    }
}