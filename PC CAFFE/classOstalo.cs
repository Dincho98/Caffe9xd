namespace PCPOS
{
    internal class classOstalo
    {
        public static string SvavkaZaPrinter(string artikl, int brojpolja)
        {
            try
            {
                string vraca = "";
                string trenutno = "";
                int lastSpace = 0;

                if (artikl.Length <= brojpolja)
                {
                    for (int i = 0; i < brojpolja; i++)
                    {
                        if (artikl.Length > i)
                        {
                            trenutno += artikl[i];
                        }
                        else
                        {
                            trenutno += " ";
                        }
                    }

                    trenutno = trenutno.TrimStart();
                    return trenutno;
                }

                int uu = 0;
                while (artikl.Substring(lastSpace).Length > brojpolja)
                {
                    string a = artikl.Substring(lastSpace, brojpolja);

                    trenutno += artikl.Substring(lastSpace, brojpolja);
                    lastSpace = trenutno.LastIndexOf(' ');
                    if (lastSpace < 0)
                    {
                        lastSpace = trenutno.Length;
                    }
                    trenutno = trenutno.Substring(0, lastSpace) + "#";
                    uu++;
                }

                trenutno = trenutno.Replace("#", "\r\n");
                trenutno = trenutno.Replace("\r\n ", "\r\n");
                trenutno = trenutno.Replace(" \r\n", "\r\n");
                trenutno = trenutno.TrimStart();

                if (artikl.Substring(lastSpace).Length <= brojpolja)
                {
                    for (int i = 0; i < brojpolja; i++)
                    {
                        if (artikl.Substring(lastSpace).Length > i)
                        {
                            if (artikl.Substring(lastSpace)[i] == ' ' && i == 0)
                            {
                                brojpolja++;
                            }
                            else
                            {
                                trenutno += artikl.Substring(lastSpace)[i];
                            }
                        }
                        else
                        {
                            trenutno += " ";
                        }
                    }
                }
                trenutno = trenutno.TrimStart();
                if (trenutno == "")
                {
                }
                return trenutno;
            }
            catch
            {
                return "";
            }
        }
    }
}