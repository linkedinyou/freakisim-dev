/*
 * Copyright (c) Contributors, http://opensimulator.org/
 * See CONTRIBUTORS.TXT for a full list of copyright holders.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *     * Redistributions of source code must retain the above copyright
 *       notice, this list of conditions and the following disclaimer.
 *     * Redistributions in binary form must reproduce the above copyright
 *       notice, this list of conditions and the following disclaimer in the
 *       documentation and/or other materials provided with the distribution.
 *     * Neither the name of the OpenSimulator Project nor the
 *       names of its contributors may be used to endorse or promote products
 *       derived from this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE DEVELOPERS ``AS IS'' AND ANY
 * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL THE CONTRIBUTORS BE LIABLE FOR ANY
 * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using OpenMetaverse;
using OpenSim.Framework;
using System;
using System.Collections.Generic;

namespace OpenSim.Region.Framework.Scenes
{
    public class EntityManager
    {
//        private static readonly ILog m_log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        
        private readonly ThreadedClasses.RwLockedDoubleDictionary<UUID, uint, IEntityBase> m_entities
            = new ThreadedClasses.RwLockedDoubleDictionary<UUID, uint, IEntityBase>();

        public int Count
        {
            get { return m_entities.Count; }
        }

        public void Add(IEntityBase entity)
        {
            m_entities.Add(entity.UUID, entity.LocalId, entity);
        }

        public void Clear()
        {
            m_entities.Clear();
        }

        public bool ContainsKey(UUID id)
        {
            return m_entities.ContainsKey(id);
        }

        public bool ContainsKey(uint localID)
        {
            return m_entities.ContainsKey(localID);
        }

        public bool Remove(uint localID)
        {
            return m_entities.Remove(localID);
        }

        public bool Remove(UUID id)
        {
            return m_entities.Remove(id);
        }

        public IEntityBase[] GetAllByType<T>()
        {
            List<IEntityBase> tmp = new List<IEntityBase>();

            ForEach(
                delegate(IEntityBase entity)
                {
                    if (entity is T)
                        tmp.Add(entity);
                }
            );

            return tmp.ToArray();
        }

        public IEntityBase[] GetEntities()
        {
            List<IEntityBase> tmp = new List<IEntityBase>(m_entities.Count);
            ForEach(delegate(IEntityBase entity) { tmp.Add(entity); });
            return tmp.ToArray();
        }

        public void ForEach(Action<IEntityBase> action)
        {
            m_entities.ForEach(action);
        }

        public IEntityBase Find(Predicate<IEntityBase> predicate)
        {
            try
            {
                m_entities.ForEach(delegate(IEntityBase eb)
                {
                    if (predicate(eb))
                    {
                        throw new ThreadedClasses.ReturnValueException<IEntityBase>(eb);
                    }
                });
            }
            catch(ThreadedClasses.ReturnValueException<IEntityBase> e)
            {
                return e.Value;
            }
            return null;
        }

        public IEntityBase this[UUID id]
        {
            get
            {
                IEntityBase entity;
                m_entities.TryGetValue(id, out entity);
                return entity;
            }
            set
            {
                Add(value);
            }
        }

        public IEntityBase this[uint localID]
        {
            get
            {
                IEntityBase entity;
                m_entities.TryGetValue(localID, out entity);
                return entity;
            }
            set
            {
                Add(value);
            }
        }

        public bool TryGetValue(UUID key, out IEntityBase obj)
        {
            return m_entities.TryGetValue(key, out obj);
        }

        public bool TryGetValue(uint key, out IEntityBase obj)
        {
            return m_entities.TryGetValue(key, out obj);
        }
    }
}
