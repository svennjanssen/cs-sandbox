using System;

namespace cs_sandbox
{
    public class OneOf<T1, T2, T3> where T1 : class where T2 : class where T3 : class {
        private T1 m_T1 { get; set; }
        private T2 m_T2 { get; set; }
        private T3 m_T3 { get; set; }

        public OneOf(T1 t1) { if (t1 == null) throw new ArgumentNullException(); m_T1 = t1; }
        public OneOf(T2 t2) { if (t2 == null) throw new ArgumentNullException(); m_T2 = t2; }
        public OneOf(T3 t3) { if (t3 == null) throw new ArgumentNullException(); m_T3 = t3; }

        public static implicit operator OneOf<T1, T2, T3>(T1 t1) => new OneOf<T1, T2, T3>(t1); 
        public static implicit operator OneOf<T1, T2, T3>(T2 t2) => new OneOf<T1, T2, T3>(t2); 
        public static implicit operator OneOf<T1, T2, T3>(T3 t3) => new OneOf<T1, T2, T3>(t3);

        public TReturn Match<TReturn>(Func<T1, TReturn> caseT1, Func<T2, TReturn> caseT2, Func<T3, TReturn> caseT3)  {
            if (m_T1 != null) return caseT1(m_T1);
            if (m_T2 != null) return caseT2(m_T2);
            if (m_T3 != null) return caseT3(m_T3);

            throw new Exception();
        }
    }
}