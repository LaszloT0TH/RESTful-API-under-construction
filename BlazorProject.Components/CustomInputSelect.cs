using Microsoft.AspNetCore.Components.Forms;

namespace BlazorProject.Components
{
    /// <summary>
    /// EN
    /// Custom InputSelect
    /// The propery way to fix this is by creating a custom InputSelect element. The easiest way to do this by inheriting the built-in component and overriding method.InputSelectTryParseValueFromString()
    /// String and Enum data types will still be supported, because in the block we are falling back to the base class implementation of method.integerELSETryParseValueFromString()
    /// GE
    /// Benutzerdefinierte InputSelect
    /// Der richtige Weg, dies zu beheben, besteht darin, ein benutzerdefiniertes InputSelect-Element zu erstellen. Der einfachste Weg, dies zu tun, indem die eingebaute Komponente geerbt und die Methode überschrieben wird.InputSelectTryParseValueFromString()
    /// String- und Enum-Datentypen werden weiterhin unterstützt, da wir im Block auf die Basisklassenimplementierung von method.integerELSETryParseValueFromString() zurückgreifen
    /// HU
    /// Egyéni bemenetSelect
    /// Ennek megoldásának propery módja egy egyéni InputSelect elem létrehozása.Ennek legegyszerűbb módja a beépített összetevő és a felülbírálási módszer öröklése.InputSelectTryParseValueFromString()
    /// A karakterlánc és az enum adattípusok továbbra is támogatottak lesznek, mert a blokkban visszaesünk a metódus alaposztály implementációjához.integerELSETryParseValueFromString()
    /// </summary>

    public class CustomInputSelect<TValue> : InputSelect<TValue>
    {
        protected override bool TryParseValueFromString(string value, out TValue result,
            out string validationErrorMessage)
        {
            if (typeof(TValue) == typeof(int))
            {
                if (int.TryParse(value, out var resultInt))
                {
                    result = (TValue)(object)resultInt;
                    validationErrorMessage = null;
                    return true;
                }
                else
                {
                    result = default;
                    validationErrorMessage =
                        $"The selected value {value} is not a valid number.";
                    return false;
                }
            }
            else
            {
                return base.TryParseValueFromString(value, out result,
                    out validationErrorMessage);
            }
        }
    }
}
