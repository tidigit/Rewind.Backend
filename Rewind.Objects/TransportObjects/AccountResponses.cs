using Rewind.Objects.TransportObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewind.Objects.TransportObjects
{

    public class AccountResponse: BaseResponse
    {
        public string AccessToken { get; set; }
        public Account User { get; set; }
    }

    public class LoginResponse: AccountResponse
    {
        public HomePage HomePage { get; set; }
    }

    public class HomePage
    {
        public List<RetrospectionStory> RetrospectionStories { get; set; }
        public List<QuickCaptureShortcut> QuickCaptureShortcuts { get; set; }

    }

    public class SignupResponse: AccountResponse
    {
        public SignupHomePage SignupHomePage { get; set; }

    }

    public class SignupHomePage
    {
        public List<OnboardingMaterial> OnboardingMaterials { get; set; }
    }
}
