//============================================================================
//SimplePend.cs Defines a class for simulating a simple pendulum
//============================================================================
using System;

namespace Sim
{
    public class SimplePend
    {
        private double len = 1.1; // pendulum length
        private double g = 9.81; // gravitaional field strength
        int n = 2;               // number of states
        private double[] x;      // array of states
        private double[] f;      // right side my equations
        private double[,] sl4;
        private double[] xi;

        //--------------------------------------------------------------------
        // constructor
        //--------------------------------------------------------------------
        public SimplePend()
        {
            x = new double [n];
            xi = new double [n];
            f = new double [n];
            sl4 = new double [n,4];

            x[0] = 1.0;
            x[1] = 0.0;

        }
        //--------------------------------------------------------------------
        // step: perform 1 integration step via Euler's Method
        // -------------------------------------------------------------------
        public void step(double dt, string method = "Euler")
        {
            rhsFunc(x,f);
            int i;
            for(i=0;i<n;++i)
            {
                if(method == "Euler")
                {
                x[i] = x[i]+f[i]*dt;
                }
                else if(method == "RK4")
                {
                    sl4[i,0]=f[i];

                    xi[i]=x[i]+sl4[i,0]*0.5*dt;
                    rhsFunc(xi,f);
                    sl4[i,1] = f[i]+.5*dt;

                    xi[i]=x[i]+sl4[i,1]*0.5*dt;
                    rhsFunc(xi,f);
                    sl4[i,2] = f[i]+.5*dt;

                    xi[i]=x[i]+sl4[i,2]*dt;
                    rhsFunc(xi,f);
                    sl4[i,3] = f[i];

                    x[i]=x[i]+(sl4[i,0]+2*sl4[i,1]+2*sl4[i,2]+sl4[i,3])*(dt/6);
                }
            }
            //Console.WriteLine($"{f[0].ToString()} {f[1].ToString()}");

        }
        //--------------------------------------------------------------------
        // rhsFunc: function to calculate rhe of pendulum equation
        // -------------------------------------------------------------------
        public void rhsFunc(double[] st, double[] ff)
        {
            ff[0] = st[1];
            ff[1] = -g/len * Math.Sin(st[0]);
        }
        //--------------------------------------------------------------------
        //Getters and Setters
        //--------------------------------------------------------------------
        public double L
        {
            get{return(len);}

            set
            {
                if(value > 0.0)
                    len = value;
            }
        }
         public double G
        {
            get{return(g);}

            set
            {
                if(value >= 0.0)
                    g = value;
            }
        }

        public double theta
        {
            get {return x[0];}

            set {x[0] = value;}
        }
        public double thetaDot
        {
            get {return x[1];}

            set {x[1] = value;}
        }
    }

}