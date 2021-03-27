import React from 'react'
import { Link } from 'react-router-dom'
import { observer } from 'mobx-react-lite';
import { List, Image, Popup } from 'semantic-ui-react'
import { Profile } from '../../../app/models/profile' 
import ProfileCard from '../../profiles/ProfileCard';

interface Props{
    attendees: Profile[];
}
export default observer(function ActivityListItemAttendee({attendees}: Props){
    return (
        <List horizontal>
            {attendees.map(attendee => (
                <Popup 
                    hoverable
                    key={attendee.username}
                    trigger={
                        <List.Item key={attendee.username} as={Link} to={`/profiles/${attendee.username}`}>
                            <Image circular size='mini' src={attendee.image || '/assets/Images/user.png'}/>
                        </List.Item>
                    }>
                    <Popup.Content>
                        <ProfileCard profile={attendee}/>
                    </Popup.Content>  
                </Popup>
            ))};
        </List>
    );
})