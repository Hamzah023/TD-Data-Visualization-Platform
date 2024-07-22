import React from 'react';
import { BrowserRouter, Route, Routes, Navigate } from 'react-router-dom';
import './App.css';
import Login from './Login';

function App() {
    return (
        <BrowserRouter>
            <div className="App">
                <Routes>
                    <Route path="/" element={<Navigate to="Form/Login" replace />} />
                    <Route path="Form/Login" element={<Login/>} />
                </Routes>
            </div>
        </BrowserRouter>
    );
}

export default App;