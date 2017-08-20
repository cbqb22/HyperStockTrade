/*
_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
_/
_/　CopyRight(C) K.Tsunoda(AddinBox) 2001 All Rights Reserved.
_/　( http://www.h3.dion.ne.jp/~sakatsu/index.htm )
_/
_/　　この祝日マクロは『kt関数アドイン』で使用しているものです。
_/　　このロジックは、レスポンスを第一義として、可能な限り少ない
_/　  【条件判定の実行】で結果を出せるように設計してあります。
_/　　この関数では、２００７年施行の改正祝日法(昭和の日)までを
_/　  サポートしています(９月の国民の休日を含む)。
_/
_/　(*1)このマクロを引用するに当たっては、必ずこのコメントも
_/　　　一緒に引用する事とします。
_/　(*2)他サイト上で本マクロを直接引用する事は、ご遠慮願います。
_/　　　【 http://www.h3.dion.ne.jp/~sakatsu/holiday_logic.htm 】
_/　　　へのリンクによる紹介で対応して下さい。
_/　(*3)[ktHolidayName]という関数名そのものは、各自の環境に
_/　　　おける命名規則に沿って変更しても構いません。
_/　
_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
*/
/*
        ＶＢＡコードのロジックを､そのまま[C ]用に書き直したものです。
        「こほろぎ AsaPi!」さんによる編集です(祝日名ではなくフラグを返す仕様です)。
*/
/*
        2005/12/03 C# 2.0 Version by Takashi Oyama (lepton@hmsoft.co.jp)
        １．判定メソッドを HolidayChecker のメンバーメソッドとした。
        　　メソッド名はそのままとした。
        ２．祝日判定メソッドの戻り値を、曜日と祝日と名称を含むクラスとした。
        注；名前空間は適当に変更して使ってください。
*/
 using System;

namespace MIC.Common.Date
{
    public static class HolidayChecker
    {
        // 春分の日を返すメソッド
        public static int Syunbun(int yy)
        {
            int dd;
            if (yy <= 1947)
            {
                dd = 99;
            }
            else if (yy <= 1979)
            {
                dd = (int)(20.8357 + (0.242194 * (yy - 1980)) - (int)((yy - 1983) / 4));
            }
            else if (yy <= 2099)
            {
                dd = (int)(20.8431 + (0.242194 * (yy - 1980)) - (int)((yy - 1980) / 4));
            }
            else if (yy <= 2150)
            {
                dd = (int)(21.851 + (0.242194 * (yy - 1980)) - (int)((yy - 1980) / 4));
            }
            else
            {
                dd = 99;
            }
            return dd;
        }

        // 秋分の日を返すメソッド
        public static int Syubun(int yy)
        {
            int dd;
            if (yy <= 1947)
            {
                dd = 99;
            }
            else if (yy <= 1979)
            {
                dd = (int)(23.2588 + (0.242194 * (yy - 1980)) - (int)((yy - 1983) / 4));
            }
            else if (yy <= 2099)
            {
                dd = (int)(23.2488 + (0.242194 * (yy - 1980)) - (int)((yy - 1980) / 4));
            }
            else if (yy <= 2150)
            {
                dd = (int)(24.2488 + (0.242194 * (yy - 1980)) - (int)((yy - 1980) / 4));
            }
            else
            {
                dd = 99;
            }
            return dd;
        }

        // 祝日情報（戻り値）
        public struct HolidayInfo
        {
            public enum HOLIDAY
            {
                WEEKDAY = 0, // 月～日
                HOLIDAY = 1, // 休日
                C_HOLIDAY = 2, // 振休
                SYUKUJITSU = 3, // 祝日

            };
            public HOLIDAY holiday; // その日の種類
            public DayOfWeek week; // その日の曜日
            public String name; // その日に名前が付いている場合はその名前。
        };
        private static readonly DateTime SYUKUJITSU = new DateTime(1948, 7, 20); // 祝日法施行日
        private static readonly DateTime FURIKAE = new DateTime(1973, 07, 12); // 振替休日制度の開始日

        // その日が何かを調べるメソッド
        public static HolidayInfo Holiday(DateTime t)
        {
            int yy = t.Year;
            int mm = t.Month;
            int dd = t.Day;
            DayOfWeek ww = t.DayOfWeek;

            HolidayInfo result = new HolidayInfo();
            result.week = ww;
            result.holiday = HolidayInfo.HOLIDAY.WEEKDAY;

            // 祝日法施行以前
            if (t < SYUKUJITSU) return result;

            switch (mm)
            {
                // １月 //
                case 1:
                    if (dd == 1)
                    {
                        result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                        result.name = "元日";      // 07/5/28 祝日名が '元旦' になっていたのを修正
                    }
                    else if (dd == 2)
                    {
                        result.name = "１月２日";
                    }
                    else if (dd == 3)
                    {
                        result.name = "１月３日";
                    }
                    else
                    {
                        if (yy >= 2000)
                        {
                            if (((int)((dd - 1) / 7) == 1) && (ww == DayOfWeek.Monday))
                            {
                                result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                                result.name = "成人の日";
                            }
                        }
                        else
                        {
                            if (dd == 15)
                            {
                                result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                                result.name = "成人の日";
                            }
                        }
                    }
                    break;
                // ２月 //
                case 2:
                    if (dd == 11)
                    {
                        if (yy >= 1967)
                        {
                            result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                            result.name = "建国記念の日";
                        }
                    }
                    else if ((yy == 1989) && (dd == 24))
                    {
                        result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                        result.name = "昭和天皇の大喪の礼";
                    }
                    break;
                // ３月 //
                case 3:
                    if (dd == Syunbun(yy))
                    {
                        result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                        result.name = "春分の日";
                    }
                    break;
                // ４月 //
                case 4:
                    if (dd == 29)
                    {
                        if (yy >= 2007)
                        {
                            result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                            result.name = "昭和の日";
                        }
                        else if (yy >= 1989)
                        {
                            result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                            result.name = "みどりの日";
                        }
                        else
                        {
                            result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                            result.name = "天皇誕生日";
                        }
                    }
                    else if ((yy == 1959) && (dd == 10))
                    {
                        result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                        result.name = "皇太子明仁親王の結婚の儀";
                    }
                    break;
                // ５月 //
                case 5:
                    if (dd == 3)
                    {
                        result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                        result.name = "憲法記念日";
                    }
                    else if (dd == 4)
                    {
                        if (yy >= 2007)
                        {
                            result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                            result.name = "みどりの日";
                        }
                        else if (yy >= 1986)
                        {
                            /* 5/4が日曜日は『只の日曜』､月曜日は『憲法記念日の振替休日』(～2006年)*/
                            if (ww > DayOfWeek.Monday)
                            {
                                result.holiday = HolidayInfo.HOLIDAY.HOLIDAY;
                                result.name = "国民の休日";
                            }
                        }
                    }
                    else if (dd == 5)
                    {
                        result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                        result.name = "こどもの日";
                    }
                    else if (dd == 6)
                    {
                        /* 07/11/21  [Wednesday or Thursday] → [Tuesday or Wednesday] に訂正 */
                        /* [5/3,5/4が日曜]ケースのみ、ここで判定 */
                        if ((yy >= 2007) && ((ww == DayOfWeek.Tuesday) || (ww == DayOfWeek.Wednesday)))
                        {
                            result.holiday = HolidayInfo.HOLIDAY.C_HOLIDAY;
                            result.name = "振替休日";
                        }
                    }
                    break;
                // ６月 //
                case 6:
                    if ((yy == 1993) && (dd == 9))
                    {
                        result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                        result.name = "皇太子徳仁親王の結婚の儀";
                    }
                    break;
                // ７月 //
                case 7:
                    if (yy >= 2003)
                    {
                        if (((int)((dd - 1) / 7) == 2) && (ww == DayOfWeek.Monday))
                        {
                            result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                            result.name = "海の日";
                        }
                    }
                    else if (yy >= 1996)
                    {
                        if (dd == 20)
                        {
                            result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                            result.name = "海の日";
                        }
                    }
                    break;
                // ８月 //
                case 8:
                    break;
                // ９月 //
                case 9:
                    if (dd == Syubun(yy))
                    {
                        result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                        result.name = "秋分の日";
                    }
                    else
                    {
                        if (yy >= 2003)
                        {
                            if (((int)((dd - 1) / 7) == 2) && (ww == DayOfWeek.Monday))
                            {
                                result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                                result.name = "敬老の日";
                            }
                            else if (ww == DayOfWeek.Tuesday)
                            {
                                if (dd == Syubun(yy) - 1)
                                {
                                    result.holiday = HolidayInfo.HOLIDAY.HOLIDAY;
                                    result.name = "国民の休日";
                                }
                            }
                        }
                        else if (yy >= 1966)
                        {
                            if (dd == 15)
                            {
                                result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                                result.name = "敬老の日";
                            }
                        }
                    }
                    break;
                // １０月 //
                case 10:
                    if (yy >= 2000)
                    {
                        if (((int)((dd - 1) / 7) == 1) && (ww == DayOfWeek.Monday))
                        {
                            result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                            result.name = "体育の日";
                        }
                    }
                    else if (yy >= 1966)
                    {
                        if (dd == 10)
                        {
                            result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                            result.name = "体育の日";
                        }
                    }
                    break;
                // １１月 //
                case 11:
                    if (dd == 3)
                    {
                        result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                        result.name = "文化の日";
                    }
                    else if (dd == 23)
                    {
                        result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                        result.name = "勤労感謝の日";
                    }
                    else if ((yy == 1990) && (dd == 12))
                    {
                        result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                        result.name = "即位礼正殿の儀";
                    }
                    break;
                // １２月 //
                case 12:
                    if (dd == 23)
                    {
                        if (yy >= 1989)
                        {
                            result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                            result.name = "天皇誕生日";
                        }
                    }
                    else if (dd == 31)
                    {
                        result.name = "１２月３１日";
                    }
                    break;
                default:
                    break;
            }

            if ((result.holiday == HolidayInfo.HOLIDAY.WEEKDAY
                 || result.holiday == HolidayInfo.HOLIDAY.HOLIDAY) &&
                (ww == DayOfWeek.Monday))
            {
                /*月曜以外は振替休日判定不要
                  5/6(火,水)の判定は上記ステップで処理済
                  5/6(月)はここで判定する  */
                if (t >= FURIKAE)
                {
                    if (Holiday(t.AddDays(-1)).holiday == HolidayInfo.HOLIDAY.SYUKUJITSU)
                    {    /* 再帰呼出 */
                        result.holiday = HolidayInfo.HOLIDAY.C_HOLIDAY;
                        result.name = "振替休日";
                    }
                }
            }
            return result;
        }

        internal static object Holiday(object date)
        {
            throw new NotImplementedException();
        }
    }
}
