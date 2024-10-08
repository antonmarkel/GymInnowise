﻿
namespace GymInnowise.EmailService.Logic.Helpers
{
    public static class TemplateVerifier
    {
        public static bool VerifyTemplateBinding(Dictionary<string, string> model, HashSet<string> template)
        {
            if (model.Count != template.Count)
            {
                return false;
            }

            foreach (var key in model.Keys)
            {
                if (!template.Contains(key))
                {
                    return false;
                }
            }

            return true;
        }
    }
}