using Finance.Application.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.Utils.Extensions
{
    public static class ValidationExtension
    {
        //public static IRuleBuilder<T, int> CheckClientIsMine<T>(this IRuleBuilder<T, int> ruleBuilder, IAppUserRepository repo, string language = "en")
        //{
        //    var options = ruleBuilder
        //        .NotNull()
        //        .Must((rootObject, id, context) =>
        //        {
        //            return repo.GetCacheList(x => x.Id == id).Any();
        //        })
        //        .WithMessage(XmlLangHtmlExtender.L("NotFound", language));
        //    return options;
        //}
    }
}
