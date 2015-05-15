﻿using System;
using System.Reflection;
using System.IO;
using System.Text;
using System.Runtime.Serialization;

using FSP = Nessos.FsPickler;

namespace Nessos.CsPickler
{
    /// <summary>
    ///     Provides basic functionality for text-based serialization.
    /// </summary>
    public abstract class CsPicklerTextSerializer : CsPicklerSerializer
    {
        FSP.FsPicklerTextSerializer _textPickler;

        /// <summary>
        ///     Wraps an FsPickler instance in a CsPickler facade.
        /// </summary>
        /// <param name="textPickler">FsPickler instance.</param>
        public CsPicklerTextSerializer(FSP.FsPicklerTextSerializer textPickler) : base(textPickler)
        {
            _textPickler = textPickler;
        }

        /// <summary>
        ///     Serializes given value to stream.
        /// </summary>
        /// <typeparam name="T">serialized value type.</typeparam>
        /// <param name="writer">target text writer.</param>
        /// <param name="value">serialized value.</param>
        /// <param name="streamingContext">payload object for StreamingContext; defaults to null.</param>
        /// <param name="leaveOpen">leave stream open; defaults to false.</param>
        public void Serialize<T>(TextWriter writer, T value, Object streamingContext = null, bool leaveOpen = false)
        {
            var sc = Utils.GetStreamingContext(streamingContext);
            var lo = Utils.GetLeaveOpen(leaveOpen);

            _textPickler.Serialize<T>(writer, value, streamingContext: sc, leaveOpen: lo);
        }

        /// <summary>
        ///     Deserializes given type from stream.
        /// </summary>
        /// <typeparam name="T">deserialized value type.</typeparam>
        /// <param name="reader">source text reader.</param>
        /// <param name="streamingContext">payload object for StreamingContext; defaults to null.</param>
        /// <param name="leaveOpen">leave stream open; defaults to false.</param>
        /// <returns>deserialized value.</returns>
        public T Deserialize<T>(TextReader reader, Object streamingContext = null, bool leaveOpen = false)
        {
            var sc = Utils.GetStreamingContext(streamingContext);
            var lo = Utils.GetLeaveOpen(leaveOpen);

            return _textPickler.Deserialize<T>(reader, streamingContext: sc, leaveOpen: lo);
        }


        /// <summary>
        ///     Creates a string pickle out of a given value.
        /// </summary>
        /// <typeparam name="T">serialized value type.</typeparam>
        /// <param name="value">serialized value.</param>
        /// <param name="streamingContext">payload object for StreamingContext; defaults to null.</param>
        /// <returns>binary pickle for object.</returns>
        public string PickleToString<T>(T value, Object streamingContext = null)
        {
            var sc = Utils.GetStreamingContext(streamingContext);

            return _textPickler.PickleToString<T>(value, streamingContext: sc);
        }

        /// <summary>
        ///     Instantiates a new object out of a string pickle.
        /// </summary>
        /// <typeparam name="T">type of value to be unpickled.</typeparam>
        /// <param name="pickle">text pickle of value.</param>
        /// <param name="streamingContext">payload object for StreamingContext; defaults to null.</param>
        /// <returns>unpickled instance.</returns>
        public T UnPickleOfString<T>(string pickle, Object streamingContext = null)
        {
            var sc = Utils.GetStreamingContext(streamingContext);

            return _textPickler.UnPickleOfString<T>(pickle, streamingContext: sc);
        }
    }
}
