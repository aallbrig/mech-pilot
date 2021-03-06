using System;
using UnityEngine;

namespace Core.Input
{
    [Serializable]
    public class TouchInteraction
    {

        private TouchInteraction(Vector2 raw)
        {
            Position = raw;
            Timing = Time.time;
        }

        public Vector2 Position { get; }

        public float Timing { get; }

        public static TouchInteraction Of(Vector2 raw) => new TouchInteraction(raw);
        public override string ToString() => $"Timing: {Timing} Position: {Position}";
    }

    [Serializable]
    public class Swipe
    {
        private readonly TouchInteraction _end;
        private readonly TouchInteraction _start;
        private Swipe(TouchInteraction start, TouchInteraction end)
        {
            _start = start;
            _end = end;
            CalculateSwipeFacts();
        }

        public float Timing { get; private set; }

        public float Distance { get; private set; }

        public Vector2 Vector { get; private set; }

        public Vector2 VectorNormalized { get; private set; }

        public static Swipe Of(TouchInteraction start, TouchInteraction end) => new Swipe(start, end);

        private void CalculateSwipeFacts()
        {
            Vector = _end.Position - _start.Position;
            VectorNormalized = Vector.normalized;
            Timing = _end.Timing - _start.Timing;
            Distance = Vector2.Distance(_end.Position, _start.Position);
        }

        public override string ToString() =>
            $"Timing: {Timing} Distance: {Distance} Vector: {Vector} Vector Normalized {VectorNormalized}";
    }

}