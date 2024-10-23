using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupAddress.Core
{
    public static class DefaultItemTemplates
    {
        public static ItemTemplate Light = new ItemTemplate("Licht allgemein",
            [
                new(2,0, "Schalten"),           new(3,0, "Schalten_Status"),

                new(6,0, "Sperren1"),           new(7,0, "Sperren_Status"),
                new(6,1, "Sperren2"),
                new(6,2, "Szene")
            ]);

        public static ItemTemplate LightDimm = new ItemTemplate("Licht Dimm_bar",
            [
                new(2,0, "Schalten"),           new(3,0, "Schalten_Status"),
                new(2,2, "Sequqenz1_Start"),    new(3,2, "Sequqenz1_Status"),
                new(2,3, "Sequqenz2_Start"),    new(3,3, "Sequqenz2_Status"),

                new(4,0, "Dimm_Hell_relativ"),
                new(4,1, "Dimm_Hell_absolut"),   new(5,1, "Dimm_Hell_Wert"),


                new(6,0, "Sperren1"),           new(7,0, "Sperren_Status"),
                new(6,1, "Sperren2"),
                new(6,2, "Szene"),
                new(6,3, "Bitszene1"),
                new(6,4, "Bitszene2"),
                new(6,5, "Bitszene3"),
                new(6,6, "Bitszene4"),
            ]);


        public static ItemTemplate LightTW = new ItemTemplate("Licht TW",
            [
                new(2,0, "Schalten"),           new(3,0, "Schalten_Status"),
                new(2,1, "HCL_Start"),          new(3,1, "HCL_Status"),
                new(2,2, "Sequqenz1_Start"),    new(3,2, "Sequqenz1_Status"),
                new(2,3, "Sequqenz2_Start"),    new(3,3, "Sequqenz2_Status"),

                new(4,0, "Dimm_Hell_relativ"),
                new(4,1, "Dimm_Hell_absolut"),   new(5,1, "Dimm_Hell_Wert"),
                new(4,2, "Dimm_Temp_P_relativ"),
                new(4,3, "Dimm_Temp_P_absolut"), new(5,3, "Dimm_Temp_P_Wert"),
                new(4,4, "Dimm_Temp_K_relativ"),
                new(4,5, "Dimm_Temp_K_absolut"), new(5,5, "Dimm_Temp_K_Wert"),


                new(6,0, "Sperren1"),           new(7,0, "Sperren_Status"),
                new(6,1, "Sperren2"),
                new(6,2, "Szene"),
                new(6,3, "Bitszene1"),
                new(6,4, "Bitszene2"),
                new(6,5, "Bitszene3"),
                new(6,6, "Bitszene4"),
            ]);


        public static ItemTemplate LightRGBW = new ItemTemplate("Licht RGBW",
            [
                new(2,  0, "Schalten"),             new(3,  0, "Schalten_Status"),
                new(2,  1, "HCL_Start"),            new(3,  1, "HCL_Status"),
                new(2,  2, "Sequqenz1_Start"),      new(3,  2, "Sequqenz1_Status"),
                new(2,  3, "Sequqenz2_Start"),      new(3,  3, "Sequqenz2_Status"),
                new(2,  4, "Sequqenz3_Start"),      new(3,  4, "Sequqenz3_Status"),
                new(2,  5, "Sequqenz4_Start"),      new(3,  5, "Sequqenz4_Status"),
                new(2,  6, "Sequqenz5_Start"),      new(3,  6, "Sequqenz5_Status"),
                new(2,  7, "Sequqenz6_Start"),      new(3,  7, "Sequqenz6_Status"),

                new(2,  8, "R_Schalten"),           new(3,  8, "R_Schalten_Status"),
                new(2,  9, "G_Schalten"),           new(3,  9, "G_Schalten_Status"),
                new(2, 10, "B_Schalten"),           new(3, 10, "B_Schalten_Status"),
                new(2, 11, "W_Schalten"),           new(3, 11, "W_Schalten_Status"),

                new(4,  0, "Dimm_H_relativ"),
                new(4,  1, "Dimm_H_absolut"),       new(5,  1, "Dimm_H_Wert"),
                new(4,  2, "Dimm_S_relativ"),
                new(4,  3, "Dimm_S_absolut"),       new(5,  3, "Dimm_S_Wert"),
                new(4,  4, "Dimm_V_relativ"),
                new(4,  5, "Dimm_V_absolut"),       new(5,  5, "Dimm_V_Wert"),

                new(4,  6, "Dimm_R_relativ"),
                new(4,  7, "Dimm_R_absolut"),       new(5,  7, "Dimm_R_Wert"),
                new(4,  8, "Dimm_G_relativ"),
                new(4,  9, "Dimm_G_absolut"),       new(5,  9, "Dimm_G_Wert"),
                new(4, 10, "Dimm_B_relativ"),
                new(4, 11, "Dimm_B_absolut"),       new(5, 11, "Dimm_B_Wert"),
                new(4, 12, "Dimm_W_relativ"),
                new(4, 13, "Dimm_W_absolut"),       new(5, 13, "Dimm_W_Wert"),


                new(4, 14, "Dimm_Temp_P_relativ"),
                new(4, 15, "Dimm_Temp_P_absolut"),  new(5, 15, "Dimm_Temp_P_Wert"),
                new(4, 16, "Dimm_Temp_K_relativ"),
                new(4, 17, "Dimm_Temp_K_absolut"),  new(5, 17, "Dimm_Temp_K_Wert"),

                new(4, 18, "HSV"),                  new(5, 18, "HSV_Wert"),
                new(4, 19, "RGB"),                  new(5, 19, "RGB_Wert"),
                                                    new(5, 20, "RGBW"),


                new(6,0, "Sperren1"),               new(7,0, "Sperren_Status"),
                new(6,1, "Sperren2"),
                new(6,2, "Szene"),
                new(6,3, "Bitszene1"),
                new(6,4, "Bitszene2"),
                new(6,5, "Bitszene3"),
                new(6,6, "Bitszene4"),
            ]);

    }
}
