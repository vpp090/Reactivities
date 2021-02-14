import React from 'react';
import { Button, Container, Menu } from 'semantic-ui-react';

interface Props{
    openForm: () => void;
}

export default function Navbar({openForm}: Props){
    return (
        <Menu inverted fixed='top'>
            <Container>
                <Menu.Item>
                    <img src="/assets/Images/logo.png" alt="logo" style={{marginRight: '10px'}}/>
                    Reactivities
                </Menu.Item>
                <Menu.Item name='Activities'>
                    <Button onClick={() => openForm()} positive content='Create Activity' />
                </Menu.Item>
            </Container>
        </Menu>
    )
}