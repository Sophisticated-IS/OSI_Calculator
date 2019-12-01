using System;
// В чем прикол этих классов, ведь они тупо выводят сообщение об ошибке, но проверку же не делают
public class Arithm_Funcs_Exception : ArithmeticException//Что нам дает это наследеование? //типо базируемся на этом классе, чтобы обраб-ть другие исключения не упомянутые дополнительно
{
    private double param1;
    private double param2;

    public double Param1Property { get; set; }//Мне это надо?//только для вывода
    public double Param2Property { get; set; }//Мне это надо?//только для вывода

    public Arithm_Funcs_Exception(string msg,double param1) :base (msg) //констр-ор для ф-ций
    {
        this.param1 = param1;
    }

    public Arithm_Funcs_Exception(string msg, double param1,double param2):this (param1)//констр-ор для арифм. операций
    {
        this.param2 = param2;
    }

}
