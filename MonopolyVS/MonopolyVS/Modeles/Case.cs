﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace MonopolyVS.Modeles
{
    public class Case
    {
        #region MEMBRES
        public int Num;
        public Rectangle Maison1;
        public Rectangle Maison2;
        public Rectangle Maison3;
        public Rectangle Maison4;
        public Rectangle NomHotel;
        public Rectangle RectAppart;
        #endregion

        #region CONSTRUCTEURS
        public Case()
        {

        }

        public Case(int numero, Rectangle rectAppart)
        {
            Num = numero;
            RectAppart = rectAppart;
        }

        public Case(int numero, Rectangle maison1, Rectangle maison2, Rectangle maison3, Rectangle maison4, Rectangle hotel, Rectangle rectAppart)
        {
            Num = numero;
            Maison1 = maison1;
            Maison2 = maison2;
            Maison3 = maison3;
            Maison4 = maison4;
            NomHotel = hotel;
            RectAppart = rectAppart;
        }
        #endregion
    }
}
