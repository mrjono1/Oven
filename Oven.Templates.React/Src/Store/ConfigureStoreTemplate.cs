using Oven.Interfaces;
using Oven.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oven.Templates.React.Src.Store
{
    /// <summary>
    /// configureStore.js Template
    /// </summary>
    public class ConfigureStoreTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public ConfigureStoreTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "configureStore.js";
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "src", "store" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            return @"import { applyMiddleware, createStore } from 'redux';
import reduxThunk from 'redux-thunk';
import createLogger from 'redux-logger';
import rootReducer from '../reducers'

export default function configureStore(initialState) {
    const logger = createLogger({
        collapsed: true,
        predicate: () => process.env.NODE_ENV === 'development'
    });

    const middleware = applyMiddleware(reduxThunk, logger);

    const store = middleware(createStore)(rootReducer, initialState);

    if (module.hot) {
        // Enable Webpack hot module replacement for reducers
        module.hot.accept('../reducers', () => {
            const nextRootReducer = require('../reducers').default;
            store.replaceReducer(nextRootReducer);
        });
    }

    return store;
}";
        }
    }
}