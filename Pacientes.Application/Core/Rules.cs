using System.Net.Mail;

namespace Pacientes.Application.Core;

public static class Rules
{
    public static string? Required(string? value, string label, int max)
    {
        if (string.IsNullOrWhiteSpace(value)) return $"{label} es requerido.";
        if (value.Trim().Length > max) return $"{label} no puede pasar de {max} caracteres.";
        return null;
    }

    public static string? Optional(string? value, string label, int max) =>
        value is not null && value.Trim().Length > max
            ? $"{label} no puede pasar de {max} caracteres."
            : null;

    public static string? Email(string? value, string label = "El correo", int max = 150)
    {
        var error = Required(value, label, max);
        if (error != null) return error;
        return MailAddress.TryCreate(value!.Trim(), out _) ? null : $"{label} no tiene un formato válido.";
    }

    public static string? BirthDate(DateTime? date) =>
        date.HasValue && date.Value.Date > DateTime.Today ? "La fecha de nacimiento no puede ser futura." : null;

    public static string? First(params string?[] errors) => errors.FirstOrDefault(e => e != null);
}
