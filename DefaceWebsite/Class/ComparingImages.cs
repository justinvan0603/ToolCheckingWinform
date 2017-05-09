using System;
using System.Drawing;
using System.Drawing.Imaging;

using System.Security.Cryptography;

namespace Imagio
{
	public class ComparingImages
	{
		public enum CompareResult
		{
			ciCompareOk,
			ciPixelMismatch,
			ciSizeMismatch
		};

        public static CompareResult CompareSHA256(Bitmap bmp1, Bitmap bmp2)
		{
			CompareResult cr = CompareResult.ciCompareOk;

			//Test to see if we have the same size of image
            //if (bmp1.Size != bmp2.Size)
            //{
            //    cr = CompareResult.ciSizeMismatch;
            //}
            //else
            //{
				//Convert each image to a byte array
				System.Drawing.ImageConverter ic = new System.Drawing.ImageConverter();
				byte[] btImage1 = new byte[1];
				btImage1 = (byte[])ic.ConvertTo(bmp1, btImage1.GetType());
				byte[] btImage2 = new byte[1];
				btImage2 = (byte[])ic.ConvertTo(bmp2, btImage2.GetType());
				
				//Compute a hash for each image
				SHA256Managed shaM = new SHA256Managed();
				byte[] hash1 = shaM.ComputeHash(btImage1);
				byte[] hash2 = shaM.ComputeHash(btImage2);
                int dif = 0;
				//Compare the hash values
				for (int i = 0; i < hash1.Length && i < hash2.Length; i++)
				{
					if (hash1[i] != hash2[i])
                    {
                        dif++;
                        cr = CompareResult.ciPixelMismatch;
                    }
				}
                double pr = (1.00 * dif) / hash1.Length * 100;
                DifCount = dif.ToString() + " - " + hash1.Length.ToString() + " - " + pr.ToString();
			//}
                shaM.Dispose();
                GC.Collect(GC.GetGeneration(shaM));
                GC.Collect(GC.GetGeneration(hash1));
                GC.Collect(GC.GetGeneration(hash2));
                GC.WaitForPendingFinalizers();
               // Array.Clear(hash1, 0, hash1.Length);
                //Array.Clear(hash2, 0, hash2.Length);
               // Array.Clear(btImage1, 0, btImage1.Length);
                //Array.Clear(btImage2, 0, btImage2.Length);
            
			return cr;
		}
        public static string DifCount = "0";
        public static CompareResult CompareMD5(Bitmap bmp1, Bitmap bmp2)
        {
            CompareResult cr = CompareResult.ciCompareOk;

            //Test to see if we have the same size of image
            //if (bmp1.Size != bmp2.Size)
            //{
            //    cr = CompareResult.ciSizeMismatch;
            //}
            //else
            //{
                //Convert each image to a byte array
                System.Drawing.ImageConverter ic = new System.Drawing.ImageConverter();
                byte[] btImage1 = new byte[1];
                btImage1 = (byte[])ic.ConvertTo(bmp1, btImage1.GetType());
                byte[] btImage2 = new byte[1];
                btImage2 = (byte[])ic.ConvertTo(bmp2, btImage2.GetType());

                //Compute a hash for each image
                MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
                byte[] hash1 = md5provider.ComputeHash(btImage1);
                byte[] hash2 = md5provider.ComputeHash(btImage2);
                int dif = 0;
                //Compare the hash values
                for (int i = 0; i < hash1.Length && i < hash2.Length ; i++)
                {
                    if (hash1[i] != hash2[i])
                    {
                        dif++;
                        cr = CompareResult.ciPixelMismatch;
                    }                        
                }
                

                double pr = (1.00*dif) / hash1.Length * 100;
                DifCount = dif.ToString() + " - " + hash1.Length.ToString() + " - " + pr.ToString();

            //}
            return cr;
        }
        //public static string MD5Hash(string input)
        //{
        //    StringBuilder hash = new StringBuilder();
        //    MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
        //    byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

        //    for (int i = 0; i < bytes.Length; i++)
        //    {
        //        hash.Append(bytes[i].ToString("x2"));
        //    }
        //    return hash.ToString();
        //}

        public static CompareResult ComparePixel(Bitmap bmp1, Bitmap bmp2)
        {
            CompareResult cr = CompareResult.ciCompareOk;

            //Test to see if we have the same size of image
            if (bmp1.Size != bmp2.Size)
            {
                cr = CompareResult.ciSizeMismatch;
            }
            else
            {
                //Sizes are the same so start comparing pixels
                for (int x = 0; x < bmp1.Width
                     && cr == CompareResult.ciCompareOk; x++)
                {
                    for (int y = 0; y < bmp1.Height
                                 && cr == CompareResult.ciCompareOk; y++)
                    {
                        if (bmp1.GetPixel(x, y) != bmp2.GetPixel(x, y))
                            cr = CompareResult.ciPixelMismatch;
                    }
                }
            }
            return cr;
        }
	}
}


