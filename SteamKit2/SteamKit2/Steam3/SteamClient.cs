﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SteamKit2
{

    public abstract class ClientMsgHandler
    {
        public string Name { get; private set; }

        protected SteamClient Client { get; private set; }


        public ClientMsgHandler( string name )
        {
            this.Name = name;
        }

        internal void Setup( SteamClient client )
        {
            this.Client = client;
        }


        internal abstract void HandleMsg( EMsg eMsg, byte[] data );
    }

    public class SteamClient : CMClient
    {
        public Dictionary<string, ClientMsgHandler> Handlers { get; private set; }

        public SteamClient()
        {
            Handlers = new Dictionary<string, ClientMsgHandler>( StringComparer.OrdinalIgnoreCase );

            AddHandler( new SteamUser() );
        }

        public void AddHandler( ClientMsgHandler handler )
        {
            Handlers[ handler.Name ] = handler;

            handler.Setup( this );
        }

        public T GetHandler<T>( string name ) where T : ClientMsgHandler
        {
            if ( Handlers.ContainsKey( name ) )
                return ( T )Handlers[ name ];

            return null;
        }

    }
}