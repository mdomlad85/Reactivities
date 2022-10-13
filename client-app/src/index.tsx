import React from 'react';
import { createRoot } from 'react-dom/client';
import './app/layout/styles.css';
import 'react-calendar/dist/Calendar.css'
import App from './app/layout/App';
import reportWebVitals from './reportWebVitals';
import {store, StoreContext} from "./app/stores/store";
import {BrowserRouter} from "react-router-dom";

const container =  document.getElementById('root');
const root = createRoot(container!);
root.render(
    <StoreContext.Provider value={store}>
        <BrowserRouter>
            <App />
        </BrowserRouter>
    </StoreContext.Provider>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
