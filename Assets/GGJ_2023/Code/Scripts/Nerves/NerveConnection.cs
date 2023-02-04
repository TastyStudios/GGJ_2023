using System;

namespace GGJ_2023.Nerves
{
    [Serializable]
    public struct NerveConnection
    {
        public NervePointType BodyPart;
        public NervePointType Sense;

        public NerveConnection(NervePointType bodyPart, NervePointType sense)
        {
            BodyPart = bodyPart;
            Sense = sense;
        }
    }
}
