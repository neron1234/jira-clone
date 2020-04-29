import React from 'react';
import logo from './logo.svg';
import $ from 'jquery';
import './App.css';

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        
        <LoginForm></LoginForm>
      </header>
      
    </div>
  );
}

class LoginForm extends React.Component {
  constructor(props) {
    super(props);
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  handleSubmit(e) {
    e.preventDefault();
    let username = e.target.elements[0].value;
    let password = e.target.elements[1].value;
    $.ajax({
      url: "https://localhost:44387/auth/login",
      mthod: 'POST',
      contentType: 'application/JSON',
      data: JSON.stringify({
        Username: username,
        Password: password
      })
    })
    .done((data) => {
      console.log(data)
    })
  }
  
  render() {
    return (
      <form onSubmit={this.handleSubmit}>
        <input type="text" placeholder="username"></input>
        <input type="password" placeholder="password"></input>
        <button type="submit" >Submit</button>
      </form>
    )
  }
}

export default App;
