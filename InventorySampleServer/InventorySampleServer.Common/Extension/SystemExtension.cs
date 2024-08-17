using Common.Enum;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.RegularExpressions;

namespace Common
{
    public static class SystemExtension
    {
        public static string GetClientIp(this HttpContext HttpContext)
        {
            #region Get Client Ip
            string IpAddressString = HttpContext.Connection.RemoteIpAddress != null ? HttpContext.Connection.RemoteIpAddress.ToString() : string.Empty;

            if (IpAddressString == null)
                return string.Empty;

            IPAddress IpAddress;
            IPAddress.TryParse(IpAddressString, out IpAddress!);

            // If we got an IPV6 address, then we need to ask the network for the IPV4 address 
            // This usually only happens when the browser is on the same machine as the server.
            if (IpAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
            {
                IpAddress = Dns.GetHostEntry(IpAddress).AddressList.First(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
            }

            return IpAddress.ToString();
            #endregion
        }

        public static string GetUserAgent(this HttpContext HttpContext)
        {
            #region Get User Agent
            return HttpContext.Request.Headers["User-Agent"].ToString();
            #endregion
        }

        public static string GetUserDeviceType(this HttpContext HttpContext)
        {
            #region Get User Device Type
            var UserAgent = HttpContext.GetUserAgent();

            if (string.IsNullOrEmpty(UserAgent))
                return string.Empty;

            if (Regex.IsMatch(UserAgent, @"GoogleTV|SmartTV|Internet.TV|NetCast|NETTV|AppleTV|boxee|Kylo|Roku|DLNADOC|CE\-HTML", RegexOptions.IgnoreCase))
            {
                // Check if user agent is a smart TV 
                return DeviceTypeEnum.TV.EnumToString();
            }
            else if (Regex.IsMatch(UserAgent, "Xbox|PLAYSTATION.3|Wii", RegexOptions.IgnoreCase))
            {
                // Check if user agent is a TV Based Gaming Console
                return DeviceTypeEnum.TV.EnumToString();
            }
            else if ((Regex.IsMatch(UserAgent, "iP(a|ro)d", RegexOptions.IgnoreCase) ||
                     (Regex.IsMatch(UserAgent, "tablet", RegexOptions.IgnoreCase)) &&
                     (!Regex.IsMatch(UserAgent, "RX-34", RegexOptions.IgnoreCase)) ||
                     (Regex.IsMatch(UserAgent, "FOLIO", RegexOptions.IgnoreCase))))
            {
                // Check if user agent is a Tablet
                return DeviceTypeEnum.Tablet.EnumToString();
            }
            else if ((Regex.IsMatch(UserAgent, "Linux", RegexOptions.IgnoreCase)) &&
                     (Regex.IsMatch(UserAgent, "Android", RegexOptions.IgnoreCase)) &&
                     (!Regex.IsMatch(UserAgent, "Fennec|mobi|HTC.Magic|HTCX06HT|Nexus.One|SC-02B|fone.945", RegexOptions.IgnoreCase)))
            {
                // Check if user agent is an Android Tablet
                return DeviceTypeEnum.Tablet.EnumToString();
            }
            else if ((Regex.IsMatch(UserAgent, "Kindle", RegexOptions.IgnoreCase)) ||
                     (Regex.IsMatch(UserAgent, "Mac.OS", RegexOptions.IgnoreCase)) &&
                     (Regex.IsMatch(UserAgent, "Silk", RegexOptions.IgnoreCase)))
            {
                // Check if user agent is a Kindle or Kindle Fire
                return DeviceTypeEnum.Tablet.EnumToString();
            }
            else if ((Regex.IsMatch(UserAgent, @"GT-P10|SC-01C|SHW-M180S|SGH-T849|SCH-I800|SHW-M180L|SPH-P100|SGH-I987|zt180|HTC(.Flyer|\\_Flyer)|Sprint.ATP51|ViewPad7|pandigital(sprnova|nova)|Ideos.S7|Dell.Streak.7|Advent.Vega|A101IT|A70BHT|MID7015|Next2|nook", RegexOptions.IgnoreCase)) ||
                     (Regex.IsMatch(UserAgent, "MB511", RegexOptions.IgnoreCase)) &&
                     (Regex.IsMatch(UserAgent, "RUTEM", RegexOptions.IgnoreCase)))
            {
                // Check if user agent is a pre Android 3.0 Tablet
                return DeviceTypeEnum.Tablet.EnumToString();
            }
            else if ((Regex.IsMatch(UserAgent, "BOLT|Fennec|Iris|Maemo|Minimo|Mobi|mowser|NetFront|Novarra|Prism|RX-34|Skyfire|Tear|XV6875|XV6975|Google.Wireless.Transcoder", RegexOptions.IgnoreCase)))
            {
                // Check if user agent is unique Mobile User Agent
                DeviceTypeEnum.Mobile.EnumToString();
            }
            else if ((Regex.IsMatch(UserAgent, "Opera", RegexOptions.IgnoreCase)) && 
                     (Regex.IsMatch(UserAgent, "Windows.NT.5", RegexOptions.IgnoreCase)) && 
                     (Regex.IsMatch(UserAgent, @"HTC|Xda|Mini|Vario|SAMSUNG\-GT\-i8000|SAMSUNG\-SGH\-i9", RegexOptions.IgnoreCase)))
            {
                // Check if user agent is an odd Opera User Agent  
                return DeviceTypeEnum.Mobile.EnumToString();
            }
            else if ((Regex.IsMatch(UserAgent, "Windows.(NT|XP|ME|9)")) && 
                     (!Regex.IsMatch(UserAgent, "Phone", RegexOptions.IgnoreCase)) || 
                     (Regex.IsMatch(UserAgent, "Win(9|.9|NT)", RegexOptions.IgnoreCase)))
            {
                // Check if user agent is Windows Desktop
                return DeviceTypeEnum.Desktop.EnumToString();
            }
            else if ((Regex.IsMatch(UserAgent, "Macintosh|PowerPC", RegexOptions.IgnoreCase)) && 
                     (!Regex.IsMatch(UserAgent, "Silk", RegexOptions.IgnoreCase)))
            {
                // Check if agent is Mac Desktop
                return DeviceTypeEnum.Desktop.EnumToString();
            }
            else if ((Regex.IsMatch(UserAgent, "Linux", RegexOptions.IgnoreCase)) && 
                     (Regex.IsMatch(UserAgent, "X11", RegexOptions.IgnoreCase)))
            {
                // Check if user agent is a Linux Desktop
                return DeviceTypeEnum.Desktop.EnumToString();
            }
            else if ((Regex.IsMatch(UserAgent, "Solaris|SunOS|BSD", RegexOptions.IgnoreCase)))
            {
                // Check if user agent is a Solaris, SunOS, BSD Desktop
                return DeviceTypeEnum.Desktop.EnumToString();
            }
            else if ((Regex.IsMatch(UserAgent, "Bot|Crawler|Spider|Yahoo|ia_archiver|Covario-IDS|findlinks|DataparkSearch|larbin|Mediapartners-Google|NG-Search|Snappy|Teoma|Jeeves|TinEye", RegexOptions.IgnoreCase)) && 
                     (!Regex.IsMatch(UserAgent, "Mobile", RegexOptions.IgnoreCase)))
            {
                // Check if user agent is a Desktop BOT/Crawler/Spider
                return DeviceTypeEnum.Desktop.EnumToString();
            }

            // Otherwise assume it is a Mobile Device
            return DeviceTypeEnum.Mobile.EnumToString();
            #endregion
        }

        public static string GetUserDeviceName(this HttpContext HttpContext)
        {
            #region Get User Device Name
            var Ip = HttpContext.GetClientIp();
            return Dns.GetHostEntry(Ip).HostName;
            #endregion
        }

        public static bool IsMobile(this HttpContext HttpContext)
        {
            #region IsMobile
            var UserAgent = HttpContext.GetUserAgent();

            if (string.IsNullOrEmpty(UserAgent))
                return false;

            //tablet
            if (Regex.IsMatch(UserAgent, "(tablet|ipad|playbook|silk)|(android(?!.*mobile))", RegexOptions.IgnoreCase))
                return true;

            //mobile
            const string mobileRegex = "blackberry|iphone|mobile|windows ce|opera mini|htc|sony|palm|symbianos|ipad|ipod|blackberry|bada|kindle|symbian|sonyericsson|android|samsung|nokia|wap|motor";

            if (Regex.IsMatch(UserAgent, mobileRegex, RegexOptions.IgnoreCase))
                return true;

            //not mobile 
            return false;
            #endregion
        }

    }
}
