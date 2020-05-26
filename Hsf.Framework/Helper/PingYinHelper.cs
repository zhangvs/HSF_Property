using Microsoft.International.Converters.PinYinConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsf.Framework.Helper
{
    /// <summary>
    /// Npinyin+微软PinYinConverter（首选Npinyin）
    /// </summary>
    public class PingYinHelper
    {
        private static Encoding gb2312 = Encoding.GetEncoding("GB2312");

        /// <summary>
        /// 汉字转全拼
        /// </summary>
        /// <param name="strChinese"></param>
        /// <returns></returns>
        public static string ConvertToAllSpell(string strChinese)
        {
            try
            {
                if (strChinese.Length != 0)
                {
                    StringBuilder fullSpell = new StringBuilder();
                    for (int i = 0; i < strChinese.Length; i++)
                    {
                        var chr = strChinese[i];
                        fullSpell.Append(GetSpell(chr));
                    }

                    return fullSpell.ToString().ToUpper();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("全拼转化出错！" + e.Message);
            }

            return string.Empty;
        }

        /// <summary>
        /// 汉字转首字母
        /// </summary>
        /// <param name="strChinese"></param>
        /// <returns></returns>
        public static string GetFirstSpell(string strChinese)
        {
            //NPinyin.Pinyin.GetInitials(strChinese)  有Bug  洺无法识别
            //return NPinyin.Pinyin.GetInitials(strChinese);

            try
            {
                if (strChinese.Length != 0)
                {
                    StringBuilder fullSpell = new StringBuilder();
                    for (int i = 0; i < strChinese.Length; i++)
                    {
                        var chr = strChinese[i];
                        fullSpell.Append(GetSpell(chr)[0]);
                    }

                    return fullSpell.ToString().ToUpper();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("首字母转化出错！" + e.Message);
            }

            return string.Empty;
        }

        private static string GetSpell(char chr)
        {
            var coverchr = NPinyin.Pinyin.GetPinyin(chr);//添加包NPinyin

            bool isChineses = ChineseChar.IsValidChar(coverchr[0]);//添加包Microsoft.PinYinConverter
            if (isChineses)
            {
                ChineseChar chineseChar = new ChineseChar(coverchr[0]);
                foreach (string value in chineseChar.Pinyins)
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        return value.Remove(value.Length - 1, 1);
                    }
                }
            }

            return coverchr;

        }

    }
}
//网上有很多说自己整理的汉字转拼音是完美的，但使用后才发现都是半吊的瓶子，问题多多。

//常见的生僻字，或多音字识别，转换后简直让人感觉可怕。

//主流的转换有三种：hash匹配，Npinyin，微软PinYinConverter。

//但单用这三个，都没法做到完美，为什么没人考虑融合呢？

//我的方案：NPinyin+微软PinYinConverter（首选Npinyin）

//微软PinYinConverter

//为什么：微软PinYinConverter很强大，但在多音字面前，犯了传统的错误，按拼音字母排序。如【强】微软居然优先【jiang】而不是】【qiang】

//所以不能优选 PinYinConverter。

//Npinyin

//很人性，很不错的第三方库，在传统多音字前优先使用率较高的，但在生僻字面前有点无法转换。（GetInitials(strChinese)  有Bug 如【洺】无法识别，但GetPinyin可以正常转换。）

//总结：优先Npinyin 翻译失败的使用微软PinYinConverter。目测完