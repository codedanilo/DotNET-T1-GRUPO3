namespace dotNET_P004;

using System.Text.RegularExpressions;

public static class Util
{
    public static bool ehDataValida(string value) 
    {
        Regex r = new Regex(@"(\d{2}\/\d{2}\/\d{4})");
        string valueAsString = value.ToString()!;
        
        return r.Match(valueAsString).Success;
    }

    public static bool ehCpfValido(string value)
    {
        return value.Length == 11;
    }

    public static bool ehSexoValido(string value)
    {
        return value.ToLower() == "m" || value.ToLower() == "masculino" 
            || value.ToLower() == "f" || value.ToLower() == "feminino";
    }

    public static bool ehDataValida(DateTime dataInicio, DateTime dataFim)
    {
        return dataInicio >= dataFim;
    }
}