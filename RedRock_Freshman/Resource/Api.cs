using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedRock_Freshman.Resource
{
    public class Api
    {
        /// <summary>
        /// 原创重邮接口，参数page size
        /// </summary>
        public const string yuanchuang_api = "http://hongyan.cqupt.edu.cn/cyxbsMobile/index.php/Home/WelcomeFreshman/cquptOriginal";

        /// <summary>
        /// 最美重邮接口，参数page size
        /// </summary>
        public const string zuimei_api = "http://hongyan.cqupt.edu.cn/cyxbsMobile/index.php/Home/WelcomeFreshman/cquptView";

        /// <summary>
        /// 优秀教师接口，参数page size
        /// </summary>
        public const string youxiujiaoshi_api = "http://hongyan.cqupt.edu.cn/cyxbsMobile/index.php/Home/WelcomeFreshman/outstandingTeacher";

        /// <summary>
        /// 优秀学子接口，参数page size
        /// </summary>
        public const string youxiuxuezi_api = "http://hongyan.cqupt.edu.cn/cyxbsMobile/index.php/Home/WelcomeFreshman/outstandingStudent";
    }
}
