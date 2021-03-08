import React from 'react'
import { Link } from 'react-router-dom'
import { Container, Header, Segment, Image, Button } from 'semantic-ui-react'

export default function HopePage(){
    return (
      <Segment inverted textAlign='center' vertical className='masthead'>
          <Container text>
                <Header as='h1' inverted>
                    <Image size='massive' src='/assets/Images/logo.png' alt='logo' style={{marginBotton: 12}} />
                    Reactivities
                </Header>
                <Header as='h2' inverted>
                    Welcome to Reactivities
                </Header>
                <Button as={Link} to='/activities' size='huge' inverted>
                    Take me to the activities
                </Button>
          </Container>
      </Segment>
    )
}