using BussinessLayer.Interfaces.ISeguridad;
using BussinessLayer.Interfaces.Language;
using BussinessLayer.Interfaces.Repository.Empresa;
using BussinessLayer.Interfaces.Repository.Seguridad;
using DataLayer.PDbContex;

namespace BussinessLayer.Services.Language.Translation
{
    public class TranslationFieldService : ITranslationFieldService
    {
        private readonly PDbContext _context;
        private readonly IUsuarioService _usuarioService;
        private readonly ITokenService _tokenService;
        private readonly IGnEmpresaRepository _gnEmpresaRepository;

        public TranslationFieldService(PDbContext context, ITokenService tokenService,
            IGnEmpresaRepository gnEmpresaRepository, IUsuarioService usuarioService)
        {
            _context = context;
            _tokenService = tokenService;
            _gnEmpresaRepository = gnEmpresaRepository;
            _usuarioService = usuarioService;
        }

        public async Task<Dictionary<string, string>> GetTranslatedFields(string tableName)
        {
            var userName = _tokenService.GetClaimValue("sub");
            var usuario = await _usuarioService.GetByUserNameResponse(userName);
            var empresa = await _gnEmpresaRepository.GetById(usuario.CompanyId);
            var languageCode = usuario.LanguageCode != empresa.LanguageCode ? usuario.LanguageCode : empresa.LanguageCode;

            _context.GnMenu.GetType().ToString();
            var translations = (from glbt in _context.GnLanguagesByTable
                                join glts in _context.GnLanguagesTableSistemas
                                    on glbt.CodigoUnico equals glts.CodigoUnico
                                where glts.TableViewName == tableName && glbt.LanguageCode == languageCode
                                select new
                                {
                                    OriginalName = glts.CodeColumnName,
                                    TranslatedName = glbt.ColumnName
                                })
                               .ToDictionary(t => t.OriginalName, t => t.TranslatedName);

            return translations;
        }
    }

}
