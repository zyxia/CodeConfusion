using System;
using System.Collections.Generic;

namespace TokenParser
{

    public class Param
    {
        public string name = "t";
        public float value = 0;
    }

    public abstract class Function
    {
        public virtual Function Reduce()
        {
            throw new NotImplementedException();
        }
        public virtual Function GetDerivative(string name)
        {
            throw new NotImplementedException();
        }

        public virtual Function Const(Param value)
        {
            throw new NotImplementedException();
        }

        public virtual Function Const(List<Param> value)
        {
            throw new NotImplementedException();
        }

        public virtual float GetValue(Param value)
        {
            throw new NotImplementedException();
        }

        public virtual float GetValue()
        {
            throw new NotImplementedException();
        }

        public virtual Function Clone()
        {
            throw new NotImplementedException();
        }

        public float GetValue(List<Param> value)
        {
            var func = Const(value);
            return func.GetValue();
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }

    public class AddFunction : Function
    {
        public Function _left;
        public Function _right;

        public AddFunction(Function left, Function right)
        {
            _left = left;
            _right = right;
        }
        public override Function Reduce()
        {
            var l= _left.Reduce();
            var r= _right.Reduce();
            if (l is ConstValueFunction && r is ConstValueFunction)
            {
                return new ConstValueFunction(l.GetValue() + r.GetValue());
            }

            if (l is ConstValueFunction && l.GetValue() == 0.0f)
            {
                return r;
            }
            if (r is ConstValueFunction && r.GetValue() == 0.0f)
            {
                return l;
            }
            _left = l;
            _right = r;
            return this;
        }
        public override Function GetDerivative(string name)
        {
            return new AddFunction(_left.GetDerivative(name), _right.GetDerivative(name));
        }

        public override Function Const(Param value)
        {
            return new AddFunction(_left.Const(value), _right.Const(value));
        }

        public override Function Const(List<Param> value)
        {
            return new AddFunction(_left.Const(value), _right.Const(value));
        }

        public override float GetValue(Param value)
        {
            return _left.GetValue(value) + _right.GetValue(value);
        }

        public override Function Clone()
        {
            return new AddFunction(_left.Clone(), _right.Clone());
        }

        public override float GetValue()
        {
            return _left.GetValue() + _right.GetValue();
        }
        public override string ToString()
        {
           return "("+_left.ToString() + " + " + _right.ToString()+")";
        }
    }

    public class SubFunction : Function
    {
        public Function _left;
        public Function _right;

        public SubFunction(Function left, Function right)
        {
            _left = left;
            _right = right;
        }

        public override Function Reduce()
        {
            var l= _left.Reduce();
            var r= _right.Reduce();
            if (l is ConstValueFunction && r is ConstValueFunction)
            {
                return new ConstValueFunction(l.GetValue() - r.GetValue());
            } 
            if (l is ConstValueFunction && l.GetValue() == 0.0f)
            {
                return new MulFunction(new ConstValueFunction(-1),r).Reduce();
            }
            if (r is ConstValueFunction && r.GetValue() == 0.0f)
            {
                return l;
            }
            _left = l;
            _right = r;
            return this;
        }
        public override Function GetDerivative(string name)
        {
            return new SubFunction(_left.GetDerivative(name), _right.GetDerivative(name));
        }

        public override Function Const(Param value)
        {
            return new SubFunction(_left.Const(value), _right.Const(value));
        }

        public override Function Const(List<Param> value)
        {
            return new SubFunction(_left.Const(value), _right.Const(value));
        }

        public override float GetValue(Param value)
        {
            return _left.GetValue(value) - _right.GetValue(value);
        }

        public override Function Clone()
        {
            return new SubFunction(_left.Clone(), _right.Clone());
        }

        public override float GetValue()
        {
            return _left.GetValue() - _right.GetValue();
        }
        public override string ToString()
        {
            return "("+_left.ToString() + " - " + _right.ToString()+")";
        }
    }

    public class MulFunction : Function
    {
        public Function _left;
        public Function _right;

        public MulFunction(Function left, Function right)
        {
            _left = left;
            _right = right;
        }
        public override Function Reduce()
        {
            var l= _left.Reduce();
            var r= _right.Reduce();
            if (l is ConstValueFunction && r is ConstValueFunction)
            {
                return new ConstValueFunction(l.GetValue() * r.GetValue());
            } 
            if (l is ConstValueFunction )
            {
                var value = l.GetValue();
                if (value == 0.0f)
                {
                    return new ConstValueFunction(0);
                }
                else if(Math.Abs(value - 1.0f) < 1e-4)
                {
                    return r;
                }
               
            }
            if (r is ConstValueFunction  )
            {
                var value = r.GetValue();
                if (value == 0.0f)
                {
                    return new ConstValueFunction(0);
                }
                else if(Math.Abs(value - 1.0f) < 1e-4)
                {
                    return l;
                }
            }
            
            _left = l;
            _right = r;
            return this;
        }

        public override Function GetDerivative(string name)
        {
            return new AddFunction(new MulFunction(_left.GetDerivative(name), _right.Clone()),
                new MulFunction(_left.Clone(), _right.GetDerivative(name)));
        }

        public override Function Const(Param value)
        {
            return new MulFunction(_left.Const(value), _right.Const(value));
        }

        public override Function Const(List<Param> value)
        {
            return new MulFunction(_left.Const(value), _right.Const(value));
        }

        public override float GetValue(Param value)
        {
            return _left.GetValue(value) * _right.GetValue(value);
        }

        public override Function Clone()
        {
            return new MulFunction(_left.Clone(), _right.Clone());
        }

        public override float GetValue()
        {
            return _left.GetValue() * _right.GetValue();
        }
        public override string ToString()
        {
            return "("+_left.ToString() + " * " + _right.ToString()+")";
        }
    }


    public class DivideFunction : Function
    {
        public Function _left;
        public Function _right;

        public DivideFunction(Function left, Function right)
        {
            _left = left;
            _right = right;
        }
        public override Function Reduce()
        {
            var l= _left.Reduce();
            var r= _right.Reduce();
            if (l is ConstValueFunction && r is ConstValueFunction)
            {
                return new ConstValueFunction(l.GetValue() / r.GetValue());
            } 
            if (l is ConstValueFunction && l.GetValue() == 0.0f)
            {
                return new ConstValueFunction(0);
            }
            if (r is ConstValueFunction && r.GetValue() == 0.0f)
            {
                throw new Exception("除以0非法！");
            }
            _left = l;
            _right = r;
            return this;
        }
        public override Function GetDerivative(string name)
        {
            var f1 = new SubFunction(new MulFunction(_left.GetDerivative(name), _right.Clone()),
                new MulFunction(_left.Clone(), _right.GetDerivative(name)));
            var f2 = new MulFunction(_right.Clone(), _right.Clone());
            return new DivideFunction(f1, f2);
        }

        public override Function Const(Param value)
        {
            return new DivideFunction(_left.Const(value), _right.Const(value));
        }

        public override Function Const(List<Param> value)
        {
            return new DivideFunction(_left.Const(value), _right.Const(value));
        }

        public override float GetValue(Param value)
        {
            return _left.GetValue(value) / _right.GetValue(value);
        }

        public override Function Clone()
        {
            return new DivideFunction(_left.Clone(), _right.Clone());
        }

        public override float GetValue()
        {
            return _left.GetValue() / _right.GetValue();
        }
        public override string ToString()
        {
            return "("+_left.ToString() + " / " + _right.ToString()+")";
        }
    }



    public class ConstValueFunction : Function
    {
        public float value { get; }

        public ConstValueFunction(float v)
        {
            value = v;
        }
        public override Function Reduce()
        { 
            return this;
        }
        public override Function GetDerivative(string name)
        {
            return new ConstValueFunction(0);
        }

        public override Function Const(Param _)
        {
            return this.Clone();
        }

        public override Function Const(List<Param> _)
        {
            return this.Clone();
        }

        public override float GetValue(Param _)
        {
            return this.value;
        }

        public override Function Clone()
        {
            return new ConstValueFunction(this.value);
        }

        public override float GetValue()
        {
            return this.value;
        }
        public override string ToString()
        {
            return this.value.ToString();
        }
    }

    public class VariableFunction : Function
    {
        public string name { get; }

        public VariableFunction(string v)
        {
            name = v;
        }
        public override Function Reduce()
        { 
            return this;
        }
        public override Function GetDerivative(string name)
        {
            if (this.name == name)
            {
                return new ConstValueFunction(1);
            }
            else
            {
                return new ConstValueFunction(0);
            }
        }

        public override Function Const(Param param)
        {
            if (this.name == param.name)
            {
                return new ConstValueFunction(param.value);
            }
            else
            {
                return new ConstValueFunction(0);
            }
        }

        public override Function Const(List<Param> paramList)
        {
            Param pFind = null;
            foreach (var param in paramList)
            {
                if (param.name == this.name)
                {
                    pFind = param;
                    break;
                }
            }

            if (pFind != null)
            {
                return new ConstValueFunction(pFind.value);
            }
            else
            {
                return new ConstValueFunction(0);
            }
        }

        public override float GetValue(Param param)
        {
            if (param.name == this.name)
                return param.value;
            throw new NotImplementedException();
        }

        public override Function Clone()
        {
            return new VariableFunction(this.name);
        }

        public override float GetValue()
        {
            throw new NotImplementedException();
        }
        public override string ToString()
        {
            return this.name;
        }
    }

    public class Sin : Function
    {
        private Function _internal;

        public Sin(Function @internal)
        {
            _internal = @internal;
        }
        public override Function Reduce()
        {
            _internal = _internal.Reduce();
            if (_internal is ConstValueFunction && this.GetValue()==0)
            {
                return new ConstValueFunction(0);
            } 
            return this;
        }
        public override Function GetDerivative(string name)
        {
            return new MulFunction(new Cos(_internal.Clone()), _internal.GetDerivative(name));
        }

        public override Function Const(Param param)
        {
            return new Sin(_internal.Const(param));
        }

        public override Function Const(List<Param> paramList)
        {
            return new Sin(_internal.Const(paramList));
        }

        public override float GetValue(Param param)
        {
            return (float) Math.Sin(_internal.GetValue(param));
        }

        public override Function Clone()
        {
            return new Sin(_internal.Clone());
        }
        public override string ToString()
        {
            return "sin("+_internal.ToString()+")";
        }
    }

    public class Cos : Function
    {
        private Function _internal;

        public Cos(Function @internal)
        {
            _internal = @internal;
        }

        public override Function Reduce()
        {
            _internal = _internal.Reduce();
            if (_internal is ConstValueFunction && this.GetValue()==0)
            {
                return new ConstValueFunction(0);
            } 
            return this;
        }
        public override Function GetDerivative(string name)
        {
            var left = new MulFunction(new ConstValueFunction(-1.0f), new Sin(_internal.Clone()));
            var right = _internal.GetDerivative(name);
            var result = new MulFunction(left, right);
            return result;
        }

        public override Function Const(Param param)
        {
            return new Cos(_internal.Const(param));
        }

        public override Function Const(List<Param> paramList)
        {
            return new Cos(_internal.Const(paramList));
        }

        public override float GetValue(Param param)
        {
            return (float) Math.Cos(_internal.GetValue(param));
        }

        public override Function Clone()
        {
            return new Cos(_internal.Clone());
        }
        public override string ToString()
        {
            return "cos("+_internal.ToString()+")";
        }
    }

}