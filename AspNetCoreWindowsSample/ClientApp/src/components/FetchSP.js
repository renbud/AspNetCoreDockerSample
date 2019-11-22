import React, { Component } from 'react';

export class FetchSP extends Component {
  static displayName = FetchSP.name;

  constructor(props) {
    super(props);
    this.state = { forecasts: [], loading: true };
  }

  componentDidMount() {
    this.populateSPData();
  }

  static renderSPTable(forecasts) {
      return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Title</th>
            <th>Details</th>
          </tr>
        </thead>
            <tbody>
                {forecasts.map((forecast,index) =>
                    <tr key={forecast.title}>
                          <td>{forecast.title}</td>
                          <td id={'htmlraw'+index}></td>
                    </tr>
                  )}
              </tbody>

      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : FetchSP.renderSPTable(this.state.forecasts);

    return (
      <div>
        <h1 id="tabelLabel" >SharePoint DWS General Data List</h1>
        <p>This component demonstrates fetching data from the DWS Sharepoint Server General Items list.</p>
        {contents}
      </div>
    );
  }

  async populateSPData() {
    const response = await fetch('sharepoint');
      const data = await response.json();
      console.log(data);

      this.setState({ forecasts: data, loading: false });

      data.map((forecast, index) => {
          console.log(forecast.title);
          console.log(forecast.details);
          document.getElementById('htmlraw' + index).innerHTML = forecast.details;
          return forecast;
        }
    )
  }
}
