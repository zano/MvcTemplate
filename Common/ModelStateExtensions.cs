using System.Collections.Generic;
using System.Web.Mvc;

namespace Common {
    public static class ModelStateExtensions {
        public static void AddModelErrors(this ModelStateDictionary modelState, string key, IEnumerable<string> errors) {
            foreach (var error in errors) modelState.AddModelError(key, error);
        }
    }
}
