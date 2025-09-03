using Ecommerce.Domain;

namespace Ecommerce.Application.Identity;

public interface IAuthService
{

    /// <summary>
    /// Q me obtenga la session global desde cualquier artefacto del proyecto
    /// </summary>
    /// <returns></returns>
    string GetSessionUser();

    /// <summary>
    /// Crear el token basado en las propiedades del usuario en session
    /// </summary>
    /// <param name="usuario"></param>
    /// <param name="roles"></param>
    /// <returns></returns>
    string CreateToken(Usuario usuario, IList<string>? roles);

}