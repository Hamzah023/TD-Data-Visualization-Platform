import React from 'react';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import './App.css';
import Login from './Login';

function App() {
    return (
        <Router>
            <div className="App">
                <Switch>
                    <Route path="/login" component={Login} />
                    <Route path="/" exact>
                        <div className="text-center">
                            <h1 className="display-4">Welcome to fileHandler!</h1>
                            <h5>This application renders text and excel files</h5>
                        </div>
                    </Route>
                </Switch>
            </div>
        </Router>
    );
}

export default App;