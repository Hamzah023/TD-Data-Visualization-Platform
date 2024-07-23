import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

  render() {
    return (
      <div>
        <h1>Hey!</h1>
        <p>Welcome to the <code> excel data visualization platform</code></p>
        <ul>
          <li><strong>Text Files</strong>. Click <em>Text File</em> to render a.txt file</li>
          <li><strong>Excel Files</strong>. click <em>Excel File</em> to render an excel (.xls) file</li>
        </ul>
      </div>
    );
  }
}
